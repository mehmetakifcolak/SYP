using Serenity.Services;

namespace SYP.Email;

public interface ISmtpSettingsSaveHandler : ISaveHandler<SmtpSettingsRow, SaveRequest<SmtpSettingsRow>, SaveResponse> { }

public class SmtpSettingsSaveHandler(IRequestContext context) :
    SaveRequestHandler<SmtpSettingsRow, SaveRequest<SmtpSettingsRow>, SaveResponse>(context),
    ISmtpSettingsSaveHandler
{
    protected override void BeforeSave()
    {
        base.BeforeSave();

        if (IsCreate)
        {
            Row.InsertDate = DateTime.Now;
            Row.InsertUserId = Convert.ToInt32(Context.User?.GetIdentifier() ?? "1");
        }
        else
        {
            Row.UpdateDate = DateTime.Now;
            Row.UpdateUserId = Convert.ToInt32(Context.User?.GetIdentifier() ?? "1");
        }

        // Eğer bu varsayılan yapılıyorsa, diğerlerini varsayılan olmaktan çıkar
        if (Row.IsDefault == true)
        {
            Connection.Execute($"UPDATE SmtpSettings SET IsDefault = 0 WHERE Id <> @Id", new { Id = Row.Id ?? 0 });
        }
    }
}
