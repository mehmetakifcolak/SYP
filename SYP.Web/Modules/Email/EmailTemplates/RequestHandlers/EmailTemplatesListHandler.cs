using Serenity.Services;

namespace SYP.Email;

public interface IEmailTemplatesListHandler : IListHandler<EmailTemplatesRow, ListRequest, ListResponse<EmailTemplatesRow>> { }

public class EmailTemplatesListHandler(IRequestContext context) :
    ListRequestHandler<EmailTemplatesRow, ListRequest, ListResponse<EmailTemplatesRow>>(context),
    IEmailTemplatesListHandler
{
}
