using Microsoft.AspNetCore.Mvc;
using Serenity.Reporting;
using Serenity.Services;
using System.Data;
using System.Globalization;

namespace SYP.Warehouse.Endpoints;

[Route("Services/Warehouse/Warehouses/[action]")]
[ConnectionKey(typeof(WarehousesRow)), ServiceAuthorize(typeof(WarehousesRow))]
public class WarehousesEndpoint : ServiceEndpoint
{
    [HttpPost, AuthorizeCreate(typeof(WarehousesRow))]
    public SaveResponse Create(IUnitOfWork uow, SaveRequest<WarehousesRow> request,
        [FromServices] IWarehousesSaveHandler handler)
    {
        return handler.Create(uow, request);
    }

    [HttpPost, AuthorizeUpdate(typeof(WarehousesRow))]
    public SaveResponse Update(IUnitOfWork uow, SaveRequest<WarehousesRow> request,
        [FromServices] IWarehousesSaveHandler handler)
    {
        return handler.Update(uow, request);
    }

    [HttpPost, AuthorizeDelete(typeof(WarehousesRow))]
    public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
        [FromServices] IWarehousesDeleteHandler handler)
    {
        return handler.Delete(uow, request);
    }

    [HttpPost]
    public RetrieveResponse<WarehousesRow> Retrieve(IDbConnection connection, RetrieveRequest request,
        [FromServices] IWarehousesRetrieveHandler handler)
    {
        return handler.Retrieve(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(WarehousesRow))]
    public ListResponse<WarehousesRow> List(IDbConnection connection, ListRequest request,
        [FromServices] IWarehousesListHandler handler)
    {
        return handler.List(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(WarehousesRow))]
    public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
        [FromServices] IWarehousesListHandler handler,
        [FromServices] IExcelExporter exporter)
    {
        var data = List(connection, request, handler).Entities;
        var bytes = exporter.Export(data, typeof(Columns.WarehousesColumns), request.ExportColumns);
        return ExcelContentResult.Create(bytes, "Warehouses_" +
            DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
    }
}
