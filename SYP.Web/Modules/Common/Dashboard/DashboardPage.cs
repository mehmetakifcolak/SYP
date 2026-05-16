using Serenity.Data;
using Dapper;
using System.Data;

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
            connection.EnsureOpen();

            // Toplam Bayi
            model.TotalDealers = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Customers");

            // Aktif Bayi
            model.ActiveDealers = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Customers WHERE IsActive = 1");

            // Pasif Bayi
            model.PassiveDealers = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Customers WHERE IsActive = 0 OR IsActive IS NULL");

            // Toplam Urun
            model.TotalProducts = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Products");

            // Son 20 Stok Hareketi
            model.RecentMovements = Dapper.SqlMapper.Query<StockMovementDto>(connection, @"
                SELECT TOP 20
                    Id, MovementType, DocumentNo, WarehouseName,
                    ProductCode, ProductName, Quantity, MovementDate, Status
                FROM StockMovements
                ORDER BY MovementDate DESC, Id DESC
            ").ToList();

            // Son 20 İşlem Logu
            model.RecentAuditLogs = Dapper.SqlMapper.Query<AuditLogDto>(connection, @"
                SELECT TOP 20
                    Id, EntityType, EntityName, ActionType, ActionDate, Username, IpAddress
                FROM AuditLog
                ORDER BY ActionDate DESC, Id DESC
            ").ToList();
        }

        return View(MVC.Views.Common.Dashboard.DashboardIndex, model);
    }
}
