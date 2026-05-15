using Serenity.Services;
using SYP.Administration;

namespace SYP.Email;

public interface IEmailTemplatesSaveHandler : ISaveHandler<EmailTemplatesRow, SaveRequest<EmailTemplatesRow>, SaveResponse> { }

public class EmailTemplatesSaveHandler(IRequestContext context) :
    SaveRequestHandler<EmailTemplatesRow, SaveRequest<EmailTemplatesRow>, SaveResponse>(context),
    IEmailTemplatesSaveHandler
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
    }
}
