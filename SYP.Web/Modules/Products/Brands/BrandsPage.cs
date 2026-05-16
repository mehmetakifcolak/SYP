namespace SYP.Products.Pages;

[PageAuthorize(typeof(BrandsRow))]
public class BrandsPage : Controller
{
    [Route("Products/Brands")]
    public ActionResult Index()
    {
        return this.GridPage<BrandsRow>("@/Products/Brands/BrandsPage");
    }
}