using Serenity.Services;

namespace SYP.Email;

public interface ISmtpSettingsRetrieveHandler : IRetrieveHandler<SmtpSettingsRow, RetrieveRequest, RetrieveResponse<SmtpSettingsRow>> { }

public class SmtpSettingsRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<SmtpSettingsRow, RetrieveRequest, RetrieveResponse<SmtpSettingsRow>>(context),
    ISmtpSettingsRetrieveHandler
{
}
