using Serenity.Services;

namespace SYP.Email;

public interface IEmailQueueListHandler : IListHandler<EmailQueueRow, ListRequest, ListResponse<EmailQueueRow>> { }

public class EmailQueueListHandler(IRequestContext context) :
    ListRequestHandler<EmailQueueRow, ListRequest, ListResponse<EmailQueueRow>>(context),
    IEmailQueueListHandler
{
}
