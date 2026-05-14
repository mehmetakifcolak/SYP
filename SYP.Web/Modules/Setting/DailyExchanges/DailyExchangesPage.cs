namespace SYP.Setting.Pages;

[PageAuthorize(typeof(DailyExchangesRow))]
public class DailyExchangesPage : Controller
{
    [Route("Setting/DailyExchanges")]
    public ActionResult Index()
    {
        return this.GridPage<DailyExchangesRow>("@/Setting/DailyExchanges/DailyExchangesPage");
    }
}