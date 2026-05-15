using Microsoft.AspNetCore.Mvc;
using Serenity.Reporting;
using Serenity.Services;
using System.Data;
using System.Globalization;

namespace SYP.Email.Endpoints;

[Route("Services/Email/EmailAttachments/[action]")]
[ConnectionKey(typeof(EmailAttachmentsRow)), ServiceAuthorize(typeof(EmailAttachmentsRow))]
public class EmailAttachmentsEndpoint : ServiceEndpoint
{
    [HttpPost]
    public RetrieveResponse<EmailAttachmentsRow> Retrieve(IDbConnection connection, RetrieveRequest request,
        [FromServices] IEmailAttachmentsRetrieveHandler handler)
    {
        return handler.Retrieve(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(EmailAttachmentsRow))]
    public ListResponse<EmailAttachmentsRow> List(IDbConnection connection, ListRequest request,
        [FromServices] IEmailAttachmentsListHandler handler)
    {
        return handler.List(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(EmailAttachmentsRow))]
    public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
        [FromServices] IEmailAttachmentsListHandler handler,
        [FromServices] IExcelExporter exporter)
    {
        var data = List(connection, request, handler).Entities;
        var bytes = exporter.Export(data, typeof(Columns.EmailAttachmentsColumns), request.ExportColumns);
        return ExcelContentResult.Create(bytes, "EmailAttachments_" +
            DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
    }
}
