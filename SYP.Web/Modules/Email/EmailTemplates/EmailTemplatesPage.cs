using Microsoft.AspNetCore.Mvc;

namespace SYP.Email.Pages;

[PageAuthorize(typeof(EmailTemplatesRow))]
public class EmailTemplatesPage : Controller
{
    [Route("Email/EmailTemplates")]
    public ActionResult Index()
    {
        return this.GridPage<EmailTemplatesRow>("@/Email/EmailTemplates/EmailTemplatesPage");
    }
}
