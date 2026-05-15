using Serenity.Services;

namespace SYP.Email;

public interface IEmailLogsRetrieveHandler : IRetrieveHandler<EmailLogsRow, RetrieveRequest, RetrieveResponse<EmailLogsRow>> { }

public class EmailLogsRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<EmailLogsRow, RetrieveRequest, RetrieveResponse<EmailLogsRow>>(context),
    IEmailLogsRetrieveHandler
{
}
