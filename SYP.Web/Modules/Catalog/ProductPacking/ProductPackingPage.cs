namespace SYP.Catalog.Pages;

[PageAuthorize(typeof(ProductPackingRow))]
public class ProductPackingPage : Controller
{
    [Route("Catalog/ProductPacking")]
    public ActionResult Index()
    {
        return this.GridPage<ProductPackingRow>("@/Catalog/ProductPacking/ProductPackingPage");
    }
}
