using Serenity.Services;

namespace SYP.Email;

public interface IEmailTemplatesSaveHandler : ISaveHandler<EmailTemplatesRow, SaveRequest<EmailTemplatesRow>, SaveResponse> { }

public class EmailTemplatesSaveHandler(IRequestContext context) :
    SaveRequestHandler<EmailTemplatesRow, SaveRequest<EmailTemplatesRow>, SaveResponse>(context),
    IEmailTemplatesSaveHandler
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
    }
}
