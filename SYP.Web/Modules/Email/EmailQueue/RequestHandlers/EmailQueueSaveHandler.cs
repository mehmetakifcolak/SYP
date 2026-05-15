using Serenity.Services;
using SYP.Administration;
using _Ext;

namespace SYP.Email;

public interface IEmailQueueSaveHandler : ISaveHandler<EmailQueueRow, SaveRequest<EmailQueueRow>, SaveResponse> { }

public class EmailQueueSaveHandler(IRequestContext context) :
    SaveRequestHandler<EmailQueueRow, SaveRequest<EmailQueueRow>, SaveResponse>(context),
    IEmailQueueSaveHandler
{
    protected override void BeforeSave()
    {
        base.BeforeSave();

        if (IsCreate)
        {
            Row.InsertDate = DateTime.Now;
            Row.InsertUserId = UserHelper.GetCurrentUserId(Connection, Context.User?.GetIdentifier());

            // Varsayılan değerler
            if (Row.Status == null)
            {
                Row.Status = Row.ScheduledAt.HasValue ? EmailQueueStatus.Scheduled : EmailQueueStatus.Pending;
            }

            if (Row.Priority == null)
            {
                Row.Priority = EmailPriority.Normal;
            }

            if (Row.RetryCount == null)
            {
                Row.RetryCount = 0;
            }
        }
    }
}
