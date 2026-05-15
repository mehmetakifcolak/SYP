using Microsoft.AspNetCore.Mvc;
using Serenity.Reporting;
using Serenity.Services;
using System.Data;
using System.Globalization;

namespace SYP.Administration.Endpoints;

[Route("Services/Administration/AuditLog/[action]")]
[ConnectionKey(typeof(AuditLogRow)), ServiceAuthorize(typeof(AuditLogRow))]
public class AuditLogEndpoint : ServiceEndpoint
{
    [HttpPost]
    public RetrieveResponse<AuditLogRow> Retrieve(IDbConnection connection, RetrieveRequest request,
        [FromServices] IAuditLogRetrieveHandler handler)
    {
        return handler.Retrieve(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(AuditLogRow))]
    public ListResponse<AuditLogRow> List(IDbConnection connection, ListRequest request,
        [FromServices] IAuditLogListHandler handler)
    {
        return handler.List(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(AuditLogRow))]
    public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
        [FromServices] IAuditLogListHandler handler,
        [FromServices] IExcelExporter exporter)
    {
        var data = List(connection, request, handler).Entities;
        var bytes = exporter.Export(data, typeof(Columns.AuditLogColumns), request.ExportColumns);
        return ExcelContentResult.Create(bytes, "AuditLog_" +
            DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
    }
}
