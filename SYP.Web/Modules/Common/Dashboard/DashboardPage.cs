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
        var today = DateTime.Today;
        var weekAgo = today.AddDays(-7);
        var monthAgo = today.AddDays(-30);

        using (var connection = sqlConnections.NewByKey("Default"))
        {
            connection.EnsureOpen();

            // === MÜŞTERİ İSTATİSTİKLERİ ===
            model.TotalDealers = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Customers");
            model.ActiveDealers = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Customers WHERE IsActive = 1");
            model.PassiveDealers = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Customers WHERE IsActive = 0 OR IsActive IS NULL");
            model.NewDealersLast30Days = connection.ExecuteScalar<int>(
                "SELECT COUNT(*) FROM Customers WHERE InsertDate >= @monthAgo", new { monthAgo });

            // === ÜRÜN İSTATİSTİKLERİ ===
            model.TotalProducts = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Products");
            model.TotalBrands = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Brands");
            model.TotalCategories = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM ProductCategory");

            // === DEPO İSTATİSTİKLERİ ===
            model.TotalWarehouses = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Warehouses");

            model.TotalStockValue = connection.ExecuteScalar<decimal?>(@"
                SELECT ISNULL(SUM(ws.Quantity * ISNULL(p.UnitPrice, 0)), 0)
                FROM WarehouseStock ws
                INNER JOIN Products p ON p.Id = ws.ProductId
            ") ?? 0;

            model.TodayEntries = connection.ExecuteScalar<int>(
                "SELECT COUNT(*) FROM StockMovements WHERE MovementType = 'Entry' AND CAST(MovementDate AS DATE) = @today", new { today });
            model.TodayExits = connection.ExecuteScalar<int>(
                "SELECT COUNT(*) FROM StockMovements WHERE MovementType = 'Exit' AND CAST(MovementDate AS DATE) = @today", new { today });
            model.WeekEntries = connection.ExecuteScalar<int>(
                "SELECT COUNT(*) FROM StockMovements WHERE MovementType = 'Entry' AND MovementDate >= @weekAgo", new { weekAgo });
            model.WeekExits = connection.ExecuteScalar<int>(
                "SELECT COUNT(*) FROM StockMovements WHERE MovementType = 'Exit' AND MovementDate >= @weekAgo", new { weekAgo });

            // === FİYAT LİSTESİ ===
            model.ActivePriceLists = connection.ExecuteScalar<int>(
                "SELECT COUNT(*) FROM PriceLists WHERE IsActive = 1 AND (ValidTo IS NULL OR ValidTo >= @today)", new { today });
            model.ExpiringPriceLists = connection.ExecuteScalar<int>(@"
                SELECT COUNT(*) FROM PriceLists
                WHERE IsActive = 1 AND ValidTo IS NOT NULL
                AND ValidTo >= @today AND ValidTo <= @monthFromNow",
                new { today, monthFromNow = today.AddDays(30) });

            // === DÜŞÜK STOK UYARISI (Miktar < 10) ===
            model.LowStockAlerts = Dapper.SqlMapper.Query<LowStockDto>(connection, @"
                SELECT TOP 10 p.Code AS ProductCode, p.Name AS ProductName,
                       w.Name AS WarehouseName, ws.Quantity
                FROM WarehouseStock ws
                INNER JOIN Products p ON p.Id = ws.ProductId
                INNER JOIN Warehouses w ON w.Id = ws.WarehouseId
                WHERE ws.Quantity > 0 AND ws.Quantity < 10
                ORDER BY ws.Quantity ASC
            ").ToList();

            // === DEPO BAZLI STOK ===
            model.StockByWarehouse = Dapper.SqlMapper.Query<WarehouseStockDto>(connection, @"
                SELECT w.Name AS WarehouseName,
                       ISNULL(SUM(ws.Quantity), 0) AS TotalQuantity,
                       COUNT(DISTINCT ws.ProductId) AS ProductCount
                FROM Warehouses w
                LEFT JOIN WarehouseStock ws ON ws.WarehouseId = w.Id
                GROUP BY w.Id, w.Name
                ORDER BY TotalQuantity DESC
            ").ToList();

            // === EN ÇOK HAREKET GÖREN ÜRÜNLER ===
            model.TopMovingProducts = Dapper.SqlMapper.Query<TopProductDto>(connection, @"
                SELECT TOP 10 ProductCode, ProductName,
                       COUNT(*) AS MovementCount,
                       SUM(ABS(Quantity)) AS TotalQuantity
                FROM StockMovements
                WHERE MovementDate >= @monthAgo
                GROUP BY ProductCode, ProductName
                ORDER BY MovementCount DESC
            ", new { monthAgo }).ToList();

            // === KATEGORİ BAZLI ÜRÜN SAYISI ===
            model.ProductsByCategory = Dapper.SqlMapper.Query<CategoryProductCountDto>(connection, @"
                SELECT TOP 10 ISNULL(c.Name, 'Kategorisiz') AS CategoryName, COUNT(*) AS ProductCount
                FROM Products p
                LEFT JOIN ProductCategory c ON c.Id = p.CategoryId
                GROUP BY c.Name
                ORDER BY ProductCount DESC
            ").ToList();

            // === MARKA BAZLI ÜRÜN SAYISI ===
            model.ProductsByBrand = Dapper.SqlMapper.Query<BrandProductCountDto>(connection, @"
                SELECT TOP 10 ISNULL(b.Name, 'Markasız') AS BrandName, COUNT(*) AS ProductCount
                FROM Products p
                LEFT JOIN Brands b ON b.Id = p.BrandId
                GROUP BY b.Name
                ORDER BY ProductCount DESC
            ").ToList();

            // === DÖVİZ KURLARI ===
            model.ExchangeRates = Dapper.SqlMapper.Query<ExchangeRateDto>(connection, @"
                SELECT CurrencyCode, ForexBuying, ForexSelling, InsertDate
                FROM DailyExchanges
                WHERE CAST(InsertDate AS DATE) = (SELECT MAX(CAST(InsertDate AS DATE)) FROM DailyExchanges)
                ORDER BY CurrencyCode
            ").ToList();

            // === SÜRESİ DOLACAK FİYAT LİSTELERİ ===
            model.ExpiringPriceListDetails = Dapper.SqlMapper.Query<ExpiringPriceListDto>(connection, @"
                SELECT Code, Name, ValidTo, DATEDIFF(DAY, @today, ValidTo) AS DaysLeft
                FROM PriceLists
                WHERE IsActive = 1 AND ValidTo IS NOT NULL
                AND ValidTo >= @today AND ValidTo <= @monthFromNow
                ORDER BY ValidTo ASC
            ", new { today, monthFromNow = today.AddDays(30) }).ToList();

            // === SON STOK HAREKETLERİ ===
            model.RecentMovements = Dapper.SqlMapper.Query<StockMovementDto>(connection, @"
                SELECT TOP 10
                    Id, MovementType, DocumentNo, WarehouseName,
                    ProductCode, ProductName, Quantity, MovementDate, Status
                FROM StockMovements
                ORDER BY MovementDate DESC, Id DESC
            ").ToList();

            // === SON İŞLEM LOGLARI ===
            model.RecentAuditLogs = Dapper.SqlMapper.Query<AuditLogDto>(connection, @"
                SELECT TOP 10
                    Id, EntityType, EntityName, ActionType, ActionDate, Username, IpAddress
                FROM AuditLog
                ORDER BY ActionDate DESC, Id DESC
            ").ToList();
        }

        return View(MVC.Views.Common.Dashboard.DashboardIndex, model);
    }
}
