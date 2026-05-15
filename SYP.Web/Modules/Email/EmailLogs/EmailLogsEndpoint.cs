using Microsoft.AspNetCore.Mvc;
using Serenity.Reporting;
using Serenity.Services;
using System.Data;
using System.Globalization;

namespace SYP.Email.Endpoints;

[Route("Services/Email/EmailLogs/[action]")]
[ConnectionKey(typeof(EmailLogsRow)), ServiceAuthorize(typeof(EmailLogsRow))]
public class EmailLogsEndpoint : ServiceEndpoint
{
    [HttpPost]
    public RetrieveResponse<EmailLogsRow> Retrieve(IDbConnection connection, RetrieveRequest request,
        [FromServices] IEmailLogsRetrieveHandler handler)
    {
        return handler.Retrieve(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(EmailLogsRow))]
    public ListResponse<EmailLogsRow> List(IDbConnection connection, ListRequest request,
        [FromServices] IEmailLogsListHandler handler)
    {
        return handler.List(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(EmailLogsRow))]
    public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
        [FromServices] IEmailLogsListHandler handler,
        [FromServices] IExcelExporter exporter)
    {
        var data = List(connection, request, handler).Entities;
        var bytes = exporter.Export(data, typeof(Columns.EmailLogsColumns), request.ExportColumns);
        return ExcelContentResult.Create(bytes, "EmailLogs_" +
            DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
    }
}
