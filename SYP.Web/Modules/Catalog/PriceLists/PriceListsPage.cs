using Microsoft.AspNetCore.Mvc;
using Serenity.Web;

namespace SYP.Catalog.Pages;

[PageAuthorize(typeof(PriceListsRow))]
public class PriceListsPage : Controller
{
    [Route("Catalog/PriceLists")]
    public ActionResult Index()
    {
        return this.GridPage<PriceListsRow>("@/Catalog/PriceLists/PriceListsGrid");
    }
}
