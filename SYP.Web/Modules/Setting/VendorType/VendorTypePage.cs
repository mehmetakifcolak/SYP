namespace SYP.Setting.Pages;

[PageAuthorize(typeof(VendorTypeRow))]
public class VendorTypePage : Controller
{
    [Route("Setting/VendorType")]
    public ActionResult Index()
    {
        return this.GridPage<VendorTypeRow>("@/Setting/VendorType/VendorTypePage");
    }
}