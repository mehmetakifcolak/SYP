using Serenity.Services;

namespace SYP.Email;

public interface IEmailQueueRetrieveHandler : IRetrieveHandler<EmailQueueRow, RetrieveRequest, RetrieveResponse<EmailQueueRow>> { }

public class EmailQueueRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<EmailQueueRow, RetrieveRequest, RetrieveResponse<EmailQueueRow>>(context),
    IEmailQueueRetrieveHandler
{
}
