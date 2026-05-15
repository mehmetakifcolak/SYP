using Microsoft.AspNetCore.Mvc;
using Serenity.Web;

namespace SYP.Warehouse.Pages;

[PageAuthorize(typeof(StockEntriesRow))]
public class StockEntriesPage : Controller
{
    [Route("Warehouse/StockEntries")]
    public ActionResult Index()
    {
        return this.GridPage<StockEntriesRow>("@/Warehouse/StockEntries/StockEntriesPage");
    }
}
