namespace SYP.Catalog.Pages;

[PageAuthorize(typeof(ProductsRow))]
public class ProductsPage : Controller
{
    [Route("Catalog/Products")]
    public ActionResult Index()
    {
        return this.GridPage<ProductsRow>("@/Catalog/Products/ProductsPage");
    }
}