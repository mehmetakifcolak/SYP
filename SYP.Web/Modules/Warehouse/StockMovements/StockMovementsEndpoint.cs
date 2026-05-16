using Microsoft.AspNetCore.Mvc;
using Serenity.Reporting;
using Serenity.Services;
using System.Data;
using System.Globalization;

namespace SYP.Warehouse.Endpoints;

[Route("Services/Warehouse/StockMovements/[action]")]
[ConnectionKey(typeof(StockMovementsRow)), ServiceAuthorize(typeof(StockMovementsRow))]
public class StockMovementsEndpoint : ServiceEndpoint
{
    [HttpPost]
    public RetrieveResponse<StockMovementsRow> Retrieve(IDbConnection connection, RetrieveRequest request,
        [FromServices] IStockMovementsRetrieveHandler handler)
    {
        return handler.Retrieve(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(StockMovementsRow))]
    public ListResponse<StockMovementsRow> List(IDbConnection connection, ListRequest request,
        [FromServices] IStockMovementsListHandler handler)
    {
        return handler.List(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(StockMovementsRow))]
    public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
        [FromServices] IStockMovementsListHandler handler,
        [FromServices] IExcelExporter exporter)
    {
        var data = List(connection, request, handler).Entities;
        var bytes = exporter.Export(data, typeof(Columns.StockMovementsColumns), request.ExportColumns);
        return ExcelContentResult.Create(bytes, "StokHareketleri_" +
            DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
    }
}
