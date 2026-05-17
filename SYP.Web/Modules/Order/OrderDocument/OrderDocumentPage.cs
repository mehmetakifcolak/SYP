namespace SYP.Order.Pages;

[PageAuthorize(typeof(OrderDocumentRow))]
public class OrderDocumentPage : Controller
{
    [Route("Order/OrderDocument")]
    public ActionResult Index()
    {
        return this.GridPage<OrderDocumentRow>("@/Order/OrderDocument/OrderDocumentPage");
    }
}