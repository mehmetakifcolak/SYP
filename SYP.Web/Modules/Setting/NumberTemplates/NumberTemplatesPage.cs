namespace SYP.Setting.Pages;

[PageAuthorize(typeof(NumberTemplatesRow))]
public class NumberTemplatesPage : Controller
{
    [Route("Setting/NumberTemplates")]
    public ActionResult Index()
    {
        return this.GridPage<NumberTemplatesRow>("@/Setting/NumberTemplates/NumberTemplatesPage");
    }
}