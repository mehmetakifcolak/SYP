using Microsoft.AspNetCore.Mvc;
using Serenity.Web;

namespace SYP.Warehouse.Pages;

[PageAuthorize(typeof(StockMovementsRow))]
public class StockMovementsPage : Controller
{
    [Route("Warehouse/StockMovements")]
    public ActionResult Index()
    {
        return this.GridPage<StockMovementsRow>("@/Warehouse/StockMovements/StockMovementsPage");
    }
}
