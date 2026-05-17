namespace SYP.Order.Pages;

[PageAuthorize(typeof(TieredDiscountSettingsRow))]
public class TieredDiscountSettingsPage : Controller
{
    [Route("Order/TieredDiscountSettings")]
    public ActionResult Index()
    {
        return this.GridPage<TieredDiscountSettingsRow>("@/Order/TieredDiscountSettings/TieredDiscountSettingsPage");
    }
}