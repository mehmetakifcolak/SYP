using Microsoft.AspNetCore.Mvc;

namespace SYP.Administration.Pages;

[PageAuthorize(typeof(AuditLogRow))]
public class AuditLogPage : Controller
{
    [Route("Administration/AuditLog")]
    public ActionResult Index()
    {
        return this.GridPage<AuditLogRow>("@/Administration/AuditLog/AuditLogPage");
    }
}
