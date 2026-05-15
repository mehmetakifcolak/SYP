using Serenity.Services;

namespace SYP.Email;

public interface IEmailAttachmentsListHandler : IListHandler<EmailAttachmentsRow, ListRequest, ListResponse<EmailAttachmentsRow>> { }

public class EmailAttachmentsListHandler(IRequestContext context) :
    ListRequestHandler<EmailAttachmentsRow, ListRequest, ListResponse<EmailAttachmentsRow>>(context),
    IEmailAttachmentsListHandler
{
}
