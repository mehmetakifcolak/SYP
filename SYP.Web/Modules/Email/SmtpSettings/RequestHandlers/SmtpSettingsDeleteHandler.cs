using Serenity.Services;

namespace SYP.Email;

public interface ISmtpSettingsDeleteHandler : IDeleteHandler<SmtpSettingsRow, DeleteRequest, DeleteResponse> { }

public class SmtpSettingsDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<SmtpSettingsRow, DeleteRequest, DeleteResponse>(context),
    ISmtpSettingsDeleteHandler
{
}
