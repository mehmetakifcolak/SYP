using Serenity.Services;

namespace SYP.Email;

public interface IEmailAttachmentsRetrieveHandler : IRetrieveHandler<EmailAttachmentsRow, RetrieveRequest, RetrieveResponse<EmailAttachmentsRow>> { }

public class EmailAttachmentsRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<EmailAttachmentsRow, RetrieveRequest, RetrieveResponse<EmailAttachmentsRow>>(context),
    IEmailAttachmentsRetrieveHandler
{
}
