using Serenity.Services;

namespace SYP.Email;

public interface IEmailTemplatesRetrieveHandler : IRetrieveHandler<EmailTemplatesRow, RetrieveRequest, RetrieveResponse<EmailTemplatesRow>> { }

public class EmailTemplatesRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<EmailTemplatesRow, RetrieveRequest, RetrieveResponse<EmailTemplatesRow>>(context),
    IEmailTemplatesRetrieveHandler
{
}
