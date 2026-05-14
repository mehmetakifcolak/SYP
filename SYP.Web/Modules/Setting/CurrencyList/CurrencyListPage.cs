namespace SYP.Setting.Pages;

[PageAuthorize(typeof(CurrencyListRow))]
public class CurrencyListPage : Controller
{
    [Route("Setting/CurrencyList")]
    public ActionResult Index()
    {
        return this.GridPage<CurrencyListRow>("@/Setting/CurrencyList/CurrencyListPage");
    }
}