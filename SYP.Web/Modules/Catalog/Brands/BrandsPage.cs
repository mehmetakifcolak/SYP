using Microsoft.AspNetCore.Mvc;
using Serenity.Web;

namespace SYP.Catalog.Pages;

[PageAuthorize(typeof(BrandsRow))]
public class BrandsPage : Controller
{
    [Route("Catalog/Brands")]
    public ActionResult Index()
    {
        return this.GridPage<BrandsRow>("@/Catalog/Brands/BrandsPage");
    }
}
