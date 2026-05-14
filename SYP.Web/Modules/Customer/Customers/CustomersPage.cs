namespace SYP.Customer.Pages;

[PageAuthorize(typeof(CustomersRow))]
public class CustomersPage : Controller
{
    [Route("Customer/Customers")]
    public ActionResult Index()
    {
        return this.GridPage<CustomersRow>("@/Customer/Customers/CustomersPage");
    }
}