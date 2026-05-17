namespace SYP.Order.Pages;

[PageAuthorize(typeof(OrderDetailRow))]
public class OrderDetailPage : Controller
{
    [Route("Order/OrderDetail")]
    public ActionResult Index()
    {
        return this.GridPage<OrderDetailRow>("@/Order/OrderDetail/OrderDetailPage");
    }
}