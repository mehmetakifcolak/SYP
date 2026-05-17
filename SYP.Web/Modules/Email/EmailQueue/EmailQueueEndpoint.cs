using Microsoft.AspNetCore.Mvc;
using Serenity.Reporting;
using Serenity.Services;
using System.Data;
using System.Globalization;

namespace SYP.Email.Endpoints;

[Route("Services/Email/EmailQueue/[action]")]
[ConnectionKey(typeof(EmailQueueRow)), ServiceAuthorize(typeof(EmailQueueRow))]
public class EmailQueueEndpoint : ServiceEndpoint
{
    [HttpPost, AuthorizeCreate(typeof(EmailQueueRow))]
    public SaveResponse Create(IUnitOfWork uow, SaveRequest<EmailQueueRow> request,
        [FromServices] IEmailQueueSaveHandler handler)
    {
        return handler.Create(uow, request);
    }

    [HttpPost, AuthorizeUpdate(typeof(EmailQueueRow))]
    public SaveResponse Update(IUnitOfWork uow, SaveRequest<EmailQueueRow> request,
        [FromServices] IEmailQueueSaveHandler handler)
    {
        return handler.Update(uow, request);
    }

    [HttpPost, AuthorizeDelete(typeof(EmailQueueRow))]
    public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
        [FromServices] IEmailQueueDeleteHandler handler)
    {
        return handler.Delete(uow, request);
    }

    [HttpPost]
    public RetrieveResponse<EmailQueueRow> Retrieve(IDbConnection connection, RetrieveRequest request,
        [FromServices] IEmailQueueRetrieveHandler handler)
    {
        return handler.Retrieve(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(EmailQueueRow))]
    public ListResponse<EmailQueueRow> List(IDbConnection connection, ListRequest request,
        [FromServices] IEmailQueueListHandler handler)
    {
        return handler.List(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(EmailQueueRow))]
    public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
        [FromServices] IEmailQueueListHandler handler,
        [FromServices] IExcelExporter exporter)
    {
        var data = List(connection, request, handler).Entities;
        var bytes = exporter.Export(data, typeof(Columns.EmailQueueColumns), request.ExportColumns);
        return ExcelContentResult.Create(bytes, "EmailQueue_" +
            DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
    }

    [HttpPost, AuthorizeUpdate(typeof(EmailQueueRow))]
    public ServiceResponse CancelEmail(IUnitOfWork uow, [FromBody] CancelEmailRequest request)
    {
        var row = uow.Connection.TryById<EmailQueueRow>(request.EmailId);
        if (row == null)
            throw new ValidationError("Email bulunamadı!");

        if (row.Status == EmailQueueStatus.Sent)
            throw new ValidationError("Gönderilmiş email iptal edilemez!");

        if (row.Status == EmailQueueStatus.Processing)
            throw new ValidationError("İşlenmekte olan email iptal edilemez!");

        uow.Connection.UpdateById(new EmailQueueRow
        {
            Id = request.EmailId,
            Status = EmailQueueStatus.Cancelled
        });

        return new ServiceResponse();
    }

    [HttpPost, AuthorizeUpdate(typeof(EmailQueueRow))]
    public ServiceResponse ResendEmail(IUnitOfWork uow, [FromBody] ResendEmailRequest request)
    {
        var row = uow.Connection.TryById<EmailQueueRow>(request.EmailId);
        if (row == null)
            throw new ValidationError("Email bulunamadı!");

        uow.Connection.UpdateById(new EmailQueueRow
        {
            Id = request.EmailId,
            Status = EmailQueueStatus.Pending,
            RetryCount = 0,
            ErrorMessage = null,
            NextRetryAt = null
        });

        return new ServiceResponse();
    }
}

public class CancelEmailRequest : ServiceRequest
{
    public long EmailId { get; set; }
}

public class ResendEmailRequest : ServiceRequest
{
    public long EmailId { get; set; }
}
