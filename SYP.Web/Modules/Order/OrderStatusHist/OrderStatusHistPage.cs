namespace SYP.Order.Pages;

[PageAuthorize(typeof(OrderStatusHistRow))]
public class OrderStatusHistPage : Controller
{
    [Route("Order/OrderStatusHist")]
    public ActionResult Index()
    {
        return this.GridPage<OrderStatusHistRow>("@/Order/OrderStatusHist/OrderStatusHistPage");
    }
}