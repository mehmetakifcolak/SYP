using Microsoft.AspNetCore.Mvc;
using Serenity.Web;

namespace SYP.Warehouse.Pages;

[PageAuthorize(typeof(WarehouseStockRow))]
public class WarehouseStockPage : Controller
{
    [Route("Warehouse/WarehouseStock")]
    public ActionResult Index()
    {
        return this.GridPage<WarehouseStockRow>("@/Warehouse/WarehouseStock/WarehouseStockPage");
    }
}
