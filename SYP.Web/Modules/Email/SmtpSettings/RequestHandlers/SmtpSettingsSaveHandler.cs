using Serenity.Services;
using SYP.Administration;

namespace SYP.Email;

public interface ISmtpSettingsSaveHandler : ISaveHandler<SmtpSettingsRow, SaveRequest<SmtpSettingsRow>, SaveResponse> { }

public class SmtpSettingsSaveHandler(IRequestContext context) :
    SaveRequestHandler<SmtpSettingsRow, SaveRequest<SmtpSettingsRow>, SaveResponse>(context),
    ISmtpSettingsSaveHandler
{
    protected override void BeforeSave()
    {
        base.BeforeSave();

        var currentUserId = UserHelper.GetCurrentUserId(Connection, Context.User?.GetIdentifier());

        if (IsCreate)
        {
            Row.InsertDate = DateTime.Now;
            Row.InsertUserId = currentUserId;
        }
        else
        {
            Row.UpdateDate = DateTime.Now;
            Row.UpdateUserId = currentUserId;
        }

        // Eğer bu varsayılan yapılıyorsa, diğerlerini varsayılan olmaktan çıkar
        if (Row.IsDefault == true)
        {
            Connection.Execute($"UPDATE SmtpSettings SET IsDefault = 0 WHERE Id <> @Id", new { Id = Row.Id ?? 0 });
        }
    }
}
