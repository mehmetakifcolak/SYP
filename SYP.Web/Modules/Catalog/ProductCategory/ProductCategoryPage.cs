namespace SYP.Catalog.Pages;

[PageAuthorize(typeof(ProductCategoryRow))]
public class ProductCategoryPage : Controller
{
    [Route("Catalog/ProductCategory")]
    public ActionResult Index()
    {
        return this.GridPage<ProductCategoryRow>("@/Catalog/ProductCategory/ProductCategoryPage");
    }
}