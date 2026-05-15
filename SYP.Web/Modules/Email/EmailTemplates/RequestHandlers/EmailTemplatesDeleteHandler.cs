using Serenity.Services;

namespace SYP.Email;

public interface IEmailTemplatesDeleteHandler : IDeleteHandler<EmailTemplatesRow, DeleteRequest, DeleteResponse> { }

public class EmailTemplatesDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<EmailTemplatesRow, DeleteRequest, DeleteResponse>(context),
    IEmailTemplatesDeleteHandler
{
}
