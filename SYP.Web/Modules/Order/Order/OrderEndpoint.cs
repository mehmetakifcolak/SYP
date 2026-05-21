using Microsoft.AspNetCore.Hosting;
using Serenity.Reporting;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using SYP.Customer.Services;
using MyRow = SYP.Order.OrderRow;

namespace SYP.Order.Endpoints;

[Route("Services/Order/Order/[action]")]
[ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
public class OrderEndpoint : ServiceEndpoint
{
    [HttpPost, AuthorizeCreate(typeof(MyRow))]
    public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
        [FromServices] IOrderSaveHandler handler)
    {
        return handler.Create(uow, request);
    }

    [HttpPost, AuthorizeUpdate(typeof(MyRow))]
    public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
        [FromServices] IOrderSaveHandler handler)
    {
        return handler.Update(uow, request);
    }
 
    [HttpPost, AuthorizeDelete(typeof(MyRow))]
    public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
        [FromServices] IOrderDeleteHandler handler)
    {
        return handler.Delete(uow, request);
    }

    [HttpPost, AuthorizeRetrieve(typeof(MyRow))]
    public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
        [FromServices] IOrderRetrieveHandler handler)
    {
        return handler.Retrieve(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(MyRow))]
    public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
        [FromServices] IOrderListHandler handler)
    {
        return handler.List(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(MyRow))]
    public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
        [FromServices] IOrderListHandler handler,
        [FromServices] IExcelExporter exporter)
    {
        var data = List(connection, request, handler).Entities;
        var bytes = exporter.Export(data, typeof(Columns.OrderColumns), request.ExportColumns);
        return ExcelContentResult.Create(bytes, "OrderList_" +
            DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
    }

    /// <summary>
    /// Mevcut Bayi kullanıcının CustomerId'sini döner.
    /// Dialog açılırken bu endpoint çağrılarak CustomerId alanı önceden doldurulabilir.
    /// </summary>
    [HttpPost]
    public GetBayiiCustomerResponse GetCurrentBayiiCustomerId(
        [FromServices] IGetBayiiCustomerService bayiiCustomerService)
    {
        var customerId = bayiiCustomerService.GetCurrentBayiiCustomerId();
        return new GetBayiiCustomerResponse { CustomerId = customerId };
    }

    /// <summary>
    /// Mevcut kullanıcının bir sipariş için yapabileceği geçiş listesini döner.
    /// </summary>
    [HttpPost, AuthorizeRetrieve(typeof(MyRow))]
    public GetAllowedTransitionsResponse GetAllowedTransitions(
        IDbConnection connection, GetAllowedTransitionsRequest request,
        [FromServices] IOrderWorkflowService workflow)
    {
        var order = connection.TryById<MyRow>(request.OrderId);
        if (order?.Status == null)
            return new GetAllowedTransitionsResponse();

        var userRole = ResolveUserRole(connection);
        var allowed  = workflow.GetAllowedTransitions(order.Status.Value, userRole);

        return new GetAllowedTransitionsResponse
        {
            Transitions = allowed.Select(s => new AllowedTransitionItem
            {
                Status        = (int)s,
                Label         = s.GetDescription(),
                RequiresReason = workflow.RequiresReason(s)
            }).ToList()
        };
    }

    private string ResolveUserRole(IDbConnection connection)
    {
        // 1. Bayi permission'ı varsa Bayi (admin kontrolünden önce yapma, admin da hepsine sahip)
        if (Permissions.HasPermission(Administration.PermissionKeys.Bayii)
            && !Permissions.HasPermission("Administration:Security"))
            return "Bayi";

        // 2. UserId parse edilemiyorsa güvenli fallback
        if (!int.TryParse(User.GetIdentifier(), out var userId))
            return "Yönetici";

        // 3. DB: Müşterinin yöneticisi mi?
        if (connection.Exists<Customer.CustomersRow>(
                new Criteria(Customer.CustomersRow.Fields.ManagerUserId) == userId))
            return "Yönetici";

        // 4. DB: Müşterinin bayi kullanıcısı mı?
        if (connection.Exists<Customer.CustomersRow>(
                new Criteria(Customer.CustomersRow.Fields.UserId) == userId))
            return "Bayi";

        // 5. Yukarıdakilerin hiçbiri uymadı → Yönetici (güncelleme yetkisi var, oturum açık)
        return "Yönetici";
    }

    /// <summary>
    /// Dekont dosyasını yükler, OrderDocument kaydı oluşturur ve siparişi DEKONT_YUKLENDI durumuna alır.
    /// </summary>
    [HttpPost, AuthorizeUpdate(typeof(MyRow))]
    public ServiceResponse UploadDekont(
        IUnitOfWork uow,
        [FromBody] UploadDekontRequest request,
        [FromServices] IWebHostEnvironment env,
        [FromServices] IOrderStatusService statusService)
    {
        if (string.IsNullOrWhiteSpace(request.FileBase64))
            throw new ValidationError("Dosya verisi bulunamadı!");

        var allowed = new[] { "image/jpeg", "image/png", "image/gif", "image/webp", "application/pdf" };
        if (!allowed.Contains(request.MimeType?.ToLower() ?? ""))
            throw new ValidationError("Sadece PDF veya resim (JPG, PNG) yükleyebilirsiniz!");

        var fileBytes = Convert.FromBase64String(request.FileBase64);
        if (fileBytes.Length > 10 * 1024 * 1024)
            throw new ValidationError("Dosya boyutu 10 MB'ı geçemez!");

        // Kayıt klasörü
        var uploadDir = Path.Combine(env.WebRootPath, "upload", "dekonts");
        Directory.CreateDirectory(uploadDir);

        var ext      = Path.GetExtension(request.FileName) ?? ".bin";
        var saved    = $"{request.OrderId}_{DateTime.Now:yyyyMMddHHmmss}{ext}";
        var fullPath = Path.Combine(uploadDir, saved);
        System.IO.File.WriteAllBytes(fullPath, fileBytes);

        var relPath = $"upload/dekonts/{saved}";

        // OrderDocument kaydı
        uow.Connection.Insert(new OrderDocumentRow
        {
            OrderId          = request.OrderId,
            DocumentType     = (int)DocumentType.Dekont,
            FileName         = request.FileName,
            FilePath         = relPath,
            FileSize         = fileBytes.Length,
            MimeType         = request.MimeType,
            UploadedByUserId = int.Parse(User.GetIdentifier()),
            UploadDate       = DateTime.Now,
            IsActive         = true
        });

        // Sipariş durumunu güncelle
        var order = uow.Connection.TryById<MyRow>(request.OrderId)
            ?? throw new ValidationError("Sipariş bulunamadı!");

        var oldStatus = order.Status;
        order.Status = OrderStatus.DEKONT_YUKLENDI;
        uow.Connection.UpdateById(order);

        // Durum değişikliğini logla
        statusService.LogStatusChange(
            uow.Connection, request.OrderId,
            oldStatus, OrderStatus.DEKONT_YUKLENDI,
            int.Parse(User.GetIdentifier()), "Yönetici/Bayi");

        return new ServiceResponse();
    }
}