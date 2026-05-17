namespace SYP.Order.Pages;

[PageAuthorize(typeof(OrderRow))]
public class OrderPage : Controller
{
    [Route("Order/Order")]
    public ActionResult Index()
    {
        return this.GridPage<OrderRow>("@/Order/Order/OrderPage");
    }
}