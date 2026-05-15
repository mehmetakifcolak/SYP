using Microsoft.AspNetCore.Mvc;
using Serenity.Reporting;
using Serenity.Services;
using System.Data;
using System.Globalization;

namespace SYP.Warehouse.Endpoints;

[Route("Services/Warehouse/StockEntries/[action]")]
[ConnectionKey(typeof(StockEntriesRow)), ServiceAuthorize(typeof(StockEntriesRow))]
public class StockEntriesEndpoint : ServiceEndpoint
{
    [HttpPost, AuthorizeCreate(typeof(StockEntriesRow))]
    public SaveResponse Create(IUnitOfWork uow, SaveRequest<StockEntriesRow> request,
        [FromServices] IStockEntriesSaveHandler handler)
    {
        return handler.Create(uow, request);
    }

    [HttpPost, AuthorizeUpdate(typeof(StockEntriesRow))]
    public SaveResponse Update(IUnitOfWork uow, SaveRequest<StockEntriesRow> request,
        [FromServices] IStockEntriesSaveHandler handler)
    {
        return handler.Update(uow, request);
    }

    [HttpPost, AuthorizeDelete(typeof(StockEntriesRow))]
    public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
        [FromServices] IStockEntriesDeleteHandler handler)
    {
        return handler.Delete(uow, request);
    }

    [HttpPost]
    public RetrieveResponse<StockEntriesRow> Retrieve(IDbConnection connection, RetrieveRequest request,
        [FromServices] IStockEntriesRetrieveHandler handler)
    {
        return handler.Retrieve(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(StockEntriesRow))]
    public ListResponse<StockEntriesRow> List(IDbConnection connection, ListRequest request,
        [FromServices] IStockEntriesListHandler handler)
    {
        return handler.List(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(StockEntriesRow))]
    public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
        [FromServices] IStockEntriesListHandler handler,
        [FromServices] IExcelExporter exporter)
    {
        var data = List(connection, request, handler).Entities;
        var bytes = exporter.Export(data, typeof(Columns.StockEntriesColumns), request.ExportColumns);
        return ExcelContentResult.Create(bytes, "StokGirisleri_" +
            DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
    }
}
