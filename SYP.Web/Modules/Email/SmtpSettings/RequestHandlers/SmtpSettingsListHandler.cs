using Serenity.Services;

namespace SYP.Email;

public interface ISmtpSettingsListHandler : IListHandler<SmtpSettingsRow, ListRequest, ListResponse<SmtpSettingsRow>> { }

public class SmtpSettingsListHandler(IRequestContext context) :
    ListRequestHandler<SmtpSettingsRow, ListRequest, ListResponse<SmtpSettingsRow>>(context),
    ISmtpSettingsListHandler
{
}
