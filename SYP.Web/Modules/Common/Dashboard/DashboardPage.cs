using Serenity.Data;
using Dapper;

namespace SYP.Common.Pages;

[Route("Dashboard/[action]")]
public class DashboardPage : Controller
{
    [PageAuthorize, HttpGet, Route("~/")]
    public ActionResult Index([FromServices] ISqlConnections sqlConnections)
    {
        var model = new DashboardPageModel();

        using (var connection = sqlConnections.NewByKey("Default"))
        {
            // Toplam Bayi
            model.TotalDealers = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Customers");

            // Aktif Bayi
            model.ActiveDealers = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Customers WHERE IsActive = 1");

            // Pasif Bayi
            model.PassiveDealers = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Customers WHERE IsActive = 0 OR IsActive IS NULL");

            // Toplam Urun
            model.TotalProducts = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Products");
        }

        return View(MVC.Views.Common.Dashboard.DashboardIndex, model);
    }
}
