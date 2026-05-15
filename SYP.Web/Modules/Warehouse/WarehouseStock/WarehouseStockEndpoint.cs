using Microsoft.AspNetCore.Mvc;
using Serenity.Reporting;
using Serenity.Services;
using System.Data;
using System.Globalization;

namespace SYP.Warehouse.Endpoints;

[Route("Services/Warehouse/WarehouseStock/[action]")]
[ConnectionKey(typeof(WarehouseStockRow)), ServiceAuthorize(typeof(WarehouseStockRow))]
public class WarehouseStockEndpoint : ServiceEndpoint
{
    [HttpPost]
    public RetrieveResponse<WarehouseStockRow> Retrieve(IDbConnection connection, RetrieveRequest request,
        [FromServices] IWarehouseStockRetrieveHandler handler)
    {
        return handler.Retrieve(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(WarehouseStockRow))]
    public ListResponse<WarehouseStockRow> List(IDbConnection connection, ListRequest request,
        [FromServices] IWarehouseStockListHandler handler)
    {
        return handler.List(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(WarehouseStockRow))]
    public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
        [FromServices] IWarehouseStockListHandler handler,
        [FromServices] IExcelExporter exporter)
    {
        var data = List(connection, request, handler).Entities;
        var bytes = exporter.Export(data, typeof(Columns.WarehouseStockColumns), request.ExportColumns);
        return ExcelContentResult.Create(bytes, "DepoStoklari_" +
            DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
    }
}
