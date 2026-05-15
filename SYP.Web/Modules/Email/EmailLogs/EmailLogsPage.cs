using Microsoft.AspNetCore.Mvc;

namespace SYP.Email.Pages;

[PageAuthorize(typeof(EmailLogsRow))]
public class EmailLogsPage : Controller
{
    [Route("Email/EmailLogs")]
    public ActionResult Index()
    {
        return this.GridPage<EmailLogsRow>("@/Email/EmailLogs/EmailLogsPage");
    }
}
