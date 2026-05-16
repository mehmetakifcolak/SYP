namespace SYP.Setting.Pages;

[PageAuthorize(typeof(CountryRow))]
public class CountryPage : Controller
{
    [Route("Setting/Country")]
    public ActionResult Index()
    {
        return this.GridPage<CountryRow>("@/Setting/Country/CountryPage");
    }
}