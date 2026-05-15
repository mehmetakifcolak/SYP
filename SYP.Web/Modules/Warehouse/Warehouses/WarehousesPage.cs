using Microsoft.AspNetCore.Mvc;

namespace SYP.Warehouse.Pages;

[PageAuthorize(typeof(WarehousesRow))]
public class WarehousesPage : Controller
{
    [Route("Warehouse/Warehouses")]
    public ActionResult Index()
    {
        return this.GridPage<WarehousesRow>("@/Warehouse/Warehouses/WarehousesPage");
    }
}
