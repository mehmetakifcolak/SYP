using Serenity.Services;

namespace SYP.Email;

public interface IEmailLogsListHandler : IListHandler<EmailLogsRow, ListRequest, ListResponse<EmailLogsRow>> { }

public class EmailLogsListHandler(IRequestContext context) :
    ListRequestHandler<EmailLogsRow, ListRequest, ListResponse<EmailLogsRow>>(context),
    IEmailLogsListHandler
{
}
