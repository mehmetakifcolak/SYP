namespace SYP.Products.Pages;

[PageAuthorize(typeof(ProductCategoryRow))]
public class ProductCategoryPage : Controller
{
    [Route("Products/ProductCategory")]
    public ActionResult Index()
    {
        return this.GridPage<ProductCategoryRow>("@/Products/ProductCategory/ProductCategoryPage");
    }
}