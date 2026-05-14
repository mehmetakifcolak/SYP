namespace SYP.Setting.Pages;

[PageAuthorize(typeof(BankAccountInformationsRow))]
public class BankAccountInformationsPage : Controller
{
    [Route("Setting/BankAccountInformations")]
    public ActionResult Index()
    {
        return this.GridPage<BankAccountInformationsRow>("@/Setting/BankAccountInformations/BankAccountInformationsPage");
    }
}