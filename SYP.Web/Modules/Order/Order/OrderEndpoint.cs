using Serenity.Reporting;
using System.Data;
using System.Globalization;
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
}