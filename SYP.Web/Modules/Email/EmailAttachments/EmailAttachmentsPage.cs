using Microsoft.AspNetCore.Mvc;

namespace SYP.Email.Pages;

[PageAuthorize(typeof(EmailAttachmentsRow))]
public class EmailAttachmentsPage : Controller
{
    [Route("Email/EmailAttachments")]
    public ActionResult Index()
    {
        return this.GridPage<EmailAttachmentsRow>("@/Email/EmailAttachments/EmailAttachmentsPage");
    }
}
