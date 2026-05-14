namespace SYP.Setting.Pages;

[PageAuthorize(typeof(VatRatesRow))]
public class VatRatesPage : Controller
{
    [Route("Setting/VatRates")]
    public ActionResult Index()
    {
        return this.GridPage<VatRatesRow>("@/Setting/VatRates/VatRatesPage");
    }
}