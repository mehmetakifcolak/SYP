using Microsoft.AspNetCore.Mvc;
using Serenity.Reporting;
using Serenity.Services;
using System.Data;
using System.Globalization;

namespace SYP.Email.Endpoints;

[Route("Services/Email/EmailTemplates/[action]")]
[ConnectionKey(typeof(EmailTemplatesRow)), ServiceAuthorize(typeof(EmailTemplatesRow))]
public class EmailTemplatesEndpoint : ServiceEndpoint
{
    [HttpPost, AuthorizeCreate(typeof(EmailTemplatesRow))]
    public SaveResponse Create(IUnitOfWork uow, SaveRequest<EmailTemplatesRow> request,
        [FromServices] IEmailTemplatesSaveHandler handler)
    {
        return handler.Create(uow, request);
    }

    [HttpPost, AuthorizeUpdate(typeof(EmailTemplatesRow))]
    public SaveResponse Update(IUnitOfWork uow, SaveRequest<EmailTemplatesRow> request,
        [FromServices] IEmailTemplatesSaveHandler handler)
    {
        return handler.Update(uow, request);
    }

    [HttpPost, AuthorizeDelete(typeof(EmailTemplatesRow))]
    public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
        [FromServices] IEmailTemplatesDeleteHandler handler)
    {
        return handler.Delete(uow, request);
    }

    [HttpPost]
    public RetrieveResponse<EmailTemplatesRow> Retrieve(IDbConnection connection, RetrieveRequest request,
        [FromServices] IEmailTemplatesRetrieveHandler handler)
    {
        return handler.Retrieve(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(EmailTemplatesRow))]
    public ListResponse<EmailTemplatesRow> List(IDbConnection connection, ListRequest request,
        [FromServices] IEmailTemplatesListHandler handler)
    {
        return handler.List(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(EmailTemplatesRow))]
    public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
        [FromServices] IEmailTemplatesListHandler handler,
        [FromServices] IExcelExporter exporter)
    {
        var data = List(connection, request, handler).Entities;
        var bytes = exporter.Export(data, typeof(Columns.EmailTemplatesColumns), request.ExportColumns);
        return ExcelContentResult.Create(bytes, "EmailTemplates_" +
            DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
    }
}
