using Microsoft.AspNetCore.Mvc;

namespace SYP.Email.Pages;

[PageAuthorize(typeof(SmtpSettingsRow))]
public class SmtpSettingsPage : Controller
{
    [Route("Email/SmtpSettings")]
    public ActionResult Index()
    {
        return this.GridPage<SmtpSettingsRow>("@/Email/SmtpSettings/SmtpSettingsPage");
    }
}
