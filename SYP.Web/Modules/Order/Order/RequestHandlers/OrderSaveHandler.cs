using Serenity;
using Serenity.Data;
using Serenity.Services;
using SYP.Email.Services;
using SYP.Setting;
using SYP.Customer.Services;
using SYP.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using MyRow = SYP.Order.OrderRow;

namespace SYP.Order;

public interface IOrderSaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class OrderSaveHandler : SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>, IOrderSaveHandler
{
    private readonly IOrderStatusService _statusService;
    private readonly IOrderWorkflowService _workflow;
    private readonly IEmailQueueSender _emailSender;
    private readonly IGetBayiiCustomerService _bayiiCustomerService;

    public OrderSaveHandler(IRequestContext context, IOrderStatusService statusService,
        IOrderWorkflowService workflow, IEmailQueueSender emailSender,
        IGetBayiiCustomerService bayiiCustomerService)
        : base(context)
    {
        _statusService = statusService;
        _workflow = workflow;
        _emailSender = emailSender;
        _bayiiCustomerService = bayiiCustomerService;
    }

    protected override void BeforeSave()
    {
        base.BeforeSave();

        if (IsCreate && !Row.CustomerId.HasValue)
        {
            if (Permissions.HasPermission(Administration.PermissionKeys.Bayii))
            {
                var bayiiCustomerId = _bayiiCustomerService.GetCurrentBayiiCustomerId();
                if (bayiiCustomerId.HasValue)
                    Row.CustomerId = bayiiCustomerId;
            }
        }

        if (IsCreate && Row.OrderNumber.IsNullOrEmpty())
            Row.OrderNumber = GenerateOrderNumber();

        if (IsCreate)
        {
            Row.OrderDate   ??= DateTime.Now;
            Row.Status      ??= OrderStatus.TALEP_BEKLETTE;
            Row.InsertDate    = DateTime.Now;
            Row.InsertUserId  = int.Parse(Context.User.GetIdentifier());
        }

        if (IsUpdate)
        {
            Row.UpdateDate   = DateTime.Now;
            Row.UpdateUserId = int.Parse(Context.User.GetIdentifier());
        }

        if (IsCreate && Row.CustomerId.HasValue && !Row.ManagerUserId.HasValue)
        {
            var customer = Connection.TryById<Customer.CustomersRow>(Row.CustomerId.Value);
            if (customer != null)
                Row.ManagerUserId = customer.ManagerUserId;
        }

        if (Row.DetailList != null && Row.DetailList.Count > 0)
            CalculateTotals();

        if (IsUpdate && Old.Status != Row.Status)
            ValidateStatusTransition();
    }

    protected override void AfterSave()
    {
        base.AfterSave();

        if (IsUpdate && Old.Status != Row.Status)
        {
            var userId   = int.Parse(Context.User.GetIdentifier());
            var userRole = GetUserRole(userId);

            _statusService.LogStatusChange(
                Connection,
                Row.Id.Value,
                Old.Status,
                Row.Status,
                userId,
                userRole,
                Row.RejectReason
            );

            SendStatusChangeEmail(Row.Status.Value);
        }

        if (IsUpdate && Row.Status == OrderStatus.HAZIRLANIYOR && Old.Status != OrderStatus.HAZIRLANIYOR)
            UpdateStockFromOrder();

        if (IsUpdate && Row.Status == OrderStatus.TESLIM_ALINDI && Old.Status != OrderStatus.TESLIM_ALINDI
            && !(Old.IsStockExitCreated ?? false))
            CreateStockExitFromOrder();
    }

    private string GenerateOrderNumber()
    {
        var template = Connection.TryFirst<NumberTemplatesRow>(q => q.SelectTableFields()
            .Where(NumberTemplatesRow.Fields.Type == (int)NumberTemplateType.B2BSiparis &
                   NumberTemplatesRow.Fields.Active == 1));

        string prefix = "B2B";
        int length = 10;

        if (template != null)
        {
            prefix = template.Prefix ?? "B2B";
            if (!template.DateFormat.IsNullOrEmpty())
            {
                prefix += DateTime.Now.ToString(template.DateFormat);
                if (!template.Suffix.IsNullOrEmpty())
                    prefix += template.Suffix;
            }
            length = prefix.Length + (template.Length ?? 5);
        }
        else
        {
            prefix = "B2B" + DateTime.Now.ToString("yyyyMM");
            length = prefix.Length + 5;
        }

        return GetNextNumberHelper.GetNextNumber(
            Connection,
            new GetNextNumberRequest { Length = length, Prefix = prefix },
            OrderRow.Fields.OrderNumber,
            OrderRow.Fields.Id
        ).Serial;
    }

    private void CalculateTotals()
    {
        foreach (var detail in Row.DetailList)
            detail.LineTotal = (detail.Quantity ?? 0) * (detail.UnitPrice ?? 0) - (detail.Discount ?? 0);

        Row.TotalAmount = Row.DetailList.Sum(d => d.LineTotal ?? 0);

        var tieredDiscount = CalculateTieredDiscount(Row.TotalAmount.Value);
        Row.DiscountPercentage = tieredDiscount.percentage;
        Row.DiscountAmount     = tieredDiscount.amount;
        Row.NetAmount          = Row.TotalAmount - Row.DiscountAmount;
    }

    private (decimal percentage, decimal amount) CalculateTieredDiscount(decimal totalAmount)
    {
        var tdsFields = TieredDiscountSettingsRow.Fields;
        var settings = Connection.List<TieredDiscountSettingsRow>(q => q
            .SelectTableFields()
            .Where(new Criteria(tdsFields.IsActive) > 0)
            .OrderBy(tdsFields.MinAmount, desc: true));

        foreach (var setting in settings)
        {
            if (setting.MinAmount.HasValue && setting.DiscountPercentage.HasValue
                && totalAmount >= setting.MinAmount.Value)
            {
                var pct = setting.DiscountPercentage.Value;
                return (pct, totalAmount * (pct / 100));
            }
        }

        return (0, 0);
    }

    private void ValidateStatusTransition()
    {
        var oldStatus = Old.Status.Value;
        var newStatus = Row.Status.Value;
        var userId    = int.Parse(Context.User.GetIdentifier());
        var userRole  = GetUserRole(userId);

        if (_workflow.RequiresReason(newStatus) && Row.RejectReason.IsNullOrEmpty())
            throw new ValidationError("Bu durum için açıklama/neden girilmesi zorunludur!");

        _workflow.ValidateTransition(oldStatus, newStatus, userRole);
    }

    private void UpdateStockFromOrder()
    {
        var detailFields = OrderDetailRow.Fields;
        var details = Connection.List<OrderDetailRow>(q => q
            .SelectTableFields()
            .Where(new Criteria(detailFields.OrderId) == Row.Id.Value));

        var warehouseId = Row.WarehouseId ?? GetDefaultWarehouseId();

        foreach (var detail in details)
        {
            var stockFields = Warehouse.WarehouseStockRow.Fields;
            var existingStock = Connection.TryFirst<Warehouse.WarehouseStockRow>(q => q
                .SelectTableFields()
                .Where(
                    new Criteria(stockFields.WarehouseId) == warehouseId &
                    new Criteria(stockFields.ProductId) == detail.ProductId.Value));

            if (existingStock != null)
            {
                var newQuantity = (existingStock.Quantity ?? 0) - (detail.Quantity ?? 0);

                if (newQuantity < 0)
                    throw new ValidationError($"Ürün '{detail.ProductCodeName}' için yeterli stok yok! Mevcut: {existingStock.Quantity}, Talep: {detail.Quantity}");

                Connection.UpdateById(new Warehouse.WarehouseStockRow
                {
                    Id             = existingStock.Id,
                    Quantity       = newQuantity,
                    LastUpdateDate = DateTime.Now
                });
            }
            else
            {
                throw new ValidationError($"Ürün '{detail.ProductCodeName}' için stok kaydı bulunamadı!");
            }
        }
    }

    private void CreateStockExitFromOrder()
    {
        var warehouseId = Row.WarehouseId ?? GetDefaultWarehouseId();

        var prefix = "SCK" + DateTime.Now.ToString("yyyyMM");
        var exitNo = GetNextNumberHelper.GetNextNumber(
            Connection,
            new GetNextNumberRequest { Length = prefix.Length + 5, Prefix = prefix },
            Warehouse.StockExitsRow.Fields.ExitNo,
            Warehouse.StockExitsRow.Fields.Id
        ).Serial;

        var userId = int.Parse(Context.User.GetIdentifier());

        var exitRow = new Warehouse.StockExitsRow
        {
            ExitNo      = exitNo,
            WarehouseId = warehouseId,
            ExitDate    = DateTime.Now,
            Description = $"Sipariş teslimi: {Row.OrderNumber}",
            Status      = Warehouse.StockExitStatus.Approved,
            InsertDate  = DateTime.Now,
            InsertUserId = userId
        };

        Connection.Insert(exitRow);
        var insertedExit = Connection.TryFirst<Warehouse.StockExitsRow>(q => q
            .Select(Warehouse.StockExitsRow.Fields.Id)
            .Where(Warehouse.StockExitsRow.Fields.ExitNo == exitNo));
        var exitId = insertedExit?.Id
            ?? throw new ValidationError("Stok çıkış kaydı oluşturulamadı.");

        var detailFields = OrderDetailRow.Fields;
        var details = Connection.List<OrderDetailRow>(q => q
            .SelectTableFields()
            .Where(
                new Criteria(detailFields.OrderId) == Row.Id.Value &
                (new Criteria(detailFields.LineStatus) == 1 |   // Onaylandı
                 new Criteria(detailFields.LineStatus) == 3))); // Revize (onaylı yeni miktar)

        foreach (var detail in details)
        {
            Connection.Insert(new Warehouse.StockExitDetailsRow
            {
                StockExitId = exitId,
                ProductId   = detail.ProductId,
                Quantity    = detail.Quantity,
                UnitPrice   = detail.UnitPrice,
                VatRate     = detail.VatRate,
                Notes       = Row.OrderNumber
            });
        }

        Connection.UpdateById(new OrderRow
        {
            Id                 = Row.Id,
            IsStockExitCreated = true,
            UpdateDate         = DateTime.Now,
            UpdateUserId       = userId
        });
    }

    private int GetDefaultWarehouseId()
    {
        var whFields = Warehouse.WarehousesRow.Fields;
        var warehouse = Connection.TryFirst<Warehouse.WarehousesRow>(q => q
            .Select(whFields.Id)
            .Where(new Criteria(whFields.IsActive) > 0)
            .OrderBy(whFields.Id));

        if (warehouse == null)
            throw new ValidationError("Sistemde aktif depo bulunamadı!");

        return warehouse.Id.Value;
    }

    private void SendStatusChangeEmail(OrderStatus newStatus)
    {
        var templateKey = _workflow.GetEmailTemplateKey(newStatus);
        if (templateKey.IsNullOrEmpty())
            return;

        var recipients = GetAllRecipientEmails();
        if (recipients.Count == 0)
            return;

        var siteUrl = "https://localhost:5001";
        var userId  = int.Parse(Context.User.GetIdentifier());
        var user    = Connection.TryById<Administration.UserRow>(userId);

        _ = _emailSender.QueueTemplateEmailAsync(new QueueTemplateEmailRequest
        {
            TemplateKey = templateKey,
            To          = recipients,
            TemplateData = new Dictionary<string, object>
            {
                { "siparis_no",           Row.OrderNumber },
                { "bayi_adi",             Row.CustomerName },
                { "toplam_tutar",         Row.NetAmount?.ToString("N2") + " " + Row.CurrencyCode },
                { "durum",                newStatus.GetDescription() },
                { "aciklama",             Row.RejectReason ?? Row.Notes },
                { "siparis_link",         $"{siteUrl}/Order/Order#{Row.Id}" },
                { "degistiren_kullanici", user?.DisplayName }
            },
            ReferenceType = "Order",
            ReferenceId   = Row.Id?.ToString()
        });
    }

    private List<string> GetAllRecipientEmails()
    {
        var emails = new List<string>();

        if (Row.CustomerId.HasValue)
        {
            var customerEmail = Connection.TryById<Customer.CustomersRow>(Row.CustomerId.Value)?.Email;
            if (!customerEmail.IsNullOrEmpty())
                emails.Add(customerEmail);
        }

        if (Row.ManagerUserId.HasValue)
        {
            var managerEmail = Connection.TryById<Administration.UserRow>(Row.ManagerUserId.Value)?.Email;
            if (!managerEmail.IsNullOrEmpty())
                emails.Add(managerEmail);
        }

        return emails;
    }

    private string GetUserRole(int userId)
    {
        // Bayii permission'ı varsa ve güvenlik yetkisi yoksa → Bayi
        if (Permissions.HasPermission(Administration.PermissionKeys.Bayii)
            && !Permissions.HasPermission("Administration:Security"))
            return "Bayi";

        // DB: yönetici mi?
        if (Connection.Exists<Customer.CustomersRow>(
                new Criteria(Customer.CustomersRow.Fields.ManagerUserId) == userId))
            return "Yönetici";

        // DB: bayi kullanıcısı mı?
        if (Connection.Exists<Customer.CustomersRow>(
                new Criteria(Customer.CustomersRow.Fields.UserId) == userId))
            return "Bayi";

        // Fallback: oturumu açık, yetkili kullanıcı → Yönetici
        return "Yönetici";
    }
}
