using Microsoft.AspNetCore.Mvc;

namespace SYP.Email.Pages;

[PageAuthorize(typeof(EmailQueueRow))]
public class EmailQueuePage : Controller
{
    [Route("Email/EmailQueue")]
    public ActionResult Index()
    {
        return this.GridPage<EmailQueueRow>("@/Email/EmailQueue/EmailQueuePage");
    }
}
