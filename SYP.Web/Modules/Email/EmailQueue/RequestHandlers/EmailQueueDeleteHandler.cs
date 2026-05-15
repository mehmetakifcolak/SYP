using Serenity.Services;

namespace SYP.Email;

public interface IEmailQueueDeleteHandler : IDeleteHandler<EmailQueueRow, DeleteRequest, DeleteResponse> { }

public class EmailQueueDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<EmailQueueRow, DeleteRequest, DeleteResponse>(context),
    IEmailQueueDeleteHandler
{
}
