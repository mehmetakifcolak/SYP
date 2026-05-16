using Microsoft.AspNetCore.Mvc;
using Serenity.Data;
using Serenity.Reporting;
using Serenity.Services;
using Serenity.Web;
using System.Data;

namespace SYP.Catalog.Endpoints;

[Route("Services/Catalog/Brands/[action]")]
[ConnectionKey(typeof(BrandsRow)), ServiceAuthorize(typeof(BrandsRow))]
public class BrandsEndpoint : ServiceEndpoint
{
    [HttpPost, AuthorizeCreate(typeof(BrandsRow))]
    public SaveResponse Create(IUnitOfWork uow, SaveRequest<BrandsRow> request,
        [FromServices] IBrandsSaveHandler handler)
    {
        return handler.Create(uow, request);
    }

    [HttpPost, AuthorizeUpdate(typeof(BrandsRow))]
    public SaveResponse Update(IUnitOfWork uow, SaveRequest<BrandsRow> request,
        [FromServices] IBrandsSaveHandler handler)
    {
        return handler.Update(uow, request);
    }

    [HttpPost, AuthorizeDelete(typeof(BrandsRow))]
    public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
        [FromServices] IBrandsDeleteHandler handler)
    {
        return handler.Delete(uow, request);
    }

    [HttpPost]
    public RetrieveResponse<BrandsRow> Retrieve(IDbConnection connection, RetrieveRequest request,
        [FromServices] IBrandsRetrieveHandler handler)
    {
        return handler.Retrieve(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(BrandsRow))]
    public ListResponse<BrandsRow> List(IDbConnection connection, ListRequest request,
        [FromServices] IBrandsListHandler handler)
    {
        return handler.List(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(BrandsRow))]
    public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
        [FromServices] IBrandsListHandler handler,
        [FromServices] IExcelExporter exporter)
    {
        var data = List(connection, request, handler).Entities;
        var bytes = exporter.Export(data, typeof(Columns.BrandsColumns), request.ExportColumns);
        return ExcelContentResult.Create(bytes, "Markalar_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
    }
}
