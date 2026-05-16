using Microsoft.AspNetCore.Mvc;
using Serenity.Web;

namespace SYP.Warehouse.Pages;

[PageAuthorize(typeof(StockExitsRow))]
public class StockExitsPage : Controller
{
    [Route("Warehouse/StockExits")]
    public ActionResult Index()
    {
        return this.GridPage<StockExitsRow>("@/Warehouse/StockExits/StockExitsPage");
    }
}
