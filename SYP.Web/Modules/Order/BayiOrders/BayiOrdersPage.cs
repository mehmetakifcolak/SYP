using Dapper;
using Serenity.Data;
using SYP.Customer.Services;

namespace SYP.Order.Pages;

[PageAuthorize("Administration:Bayii")]
public class BayiOrdersPage : Controller
{
    [HttpGet, Route("Order/BayiOrders")]
    public ActionResult Index(
        [FromServices] ISqlConnections sqlConnections,
        [FromServices] IGetBayiiCustomerService bayiiCustomerService)
    {
        var customerId = bayiiCustomerService.GetCurrentBayiiCustomerId();
        if (!customerId.HasValue)
            return Content("Bayii müşteri kaydı bulunamadı.");

        var model = new BayiOrdersPageModel();

        using var connection = sqlConnections.NewByKey("Default");
        connection.EnsureOpen();

        model.CustomerName = connection.ExecuteScalar<string>(
            "SELECT Name FROM Customers WHERE Id = @id", new { id = customerId.Value }) ?? "";

        model.TotalOrders = connection.ExecuteScalar<int>(
            "SELECT COUNT(*) FROM Orders WHERE CustomerId = @id", new { id = customerId.Value });

        // Terminal olmayan durumlar: 9 (Teslim), 10 (İptal), 13 (Teslim Alınmadı)
        model.ActiveOrders = connection.ExecuteScalar<int>(
            "SELECT COUNT(*) FROM Orders WHERE CustomerId = @id AND Status NOT IN (9, 10, 13)",
            new { id = customerId.Value });

        // Onay bekleyenler: 1 (Talep Gönderildi), 14 (Talep Beklette)
        model.PendingApproval = connection.ExecuteScalar<int>(
            "SELECT COUNT(*) FROM Orders WHERE CustomerId = @id AND Status IN (1, 14)",
            new { id = customerId.Value });

        model.DeliveredOrders = connection.ExecuteScalar<int>(
            "SELECT COUNT(*) FROM Orders WHERE CustomerId = @id AND Status = 9",
            new { id = customerId.Value });

        // İptal/Red: 4 (Bayi Reddetti), 6 (Dekont Red), 10 (İptal), 13 (Teslim Alınmadı)
        model.CancelledOrders = connection.ExecuteScalar<int>(
            "SELECT COUNT(*) FROM Orders WHERE CustomerId = @id AND Status IN (4, 6, 10, 13)",
            new { id = customerId.Value });

        model.StatusCounts = SqlMapper.Query<BayiOrderStatusCountDto>(connection, @"
            SELECT Status, COUNT(*) AS Count
            FROM Orders WHERE CustomerId = @id
            GROUP BY Status
            ORDER BY Status",
            new { id = customerId.Value }).ToList();

        model.RecentOrders = SqlMapper.Query<BayiRecentOrderDto>(connection, @"
            SELECT TOP 50 o.Id, o.OrderNumber, o.OrderDate,
                   ISNULL(o.NetAmount, 0) AS NetAmount,
                   ISNULL(cl.Code, 'TRY') AS CurrencyCode,
                   o.Status
            FROM Orders o
            LEFT JOIN CurrencyList cl ON cl.Id = o.CurrencyId
            WHERE o.CustomerId = @id
            ORDER BY o.OrderDate DESC, o.Id DESC",
            new { id = customerId.Value }).ToList();

        return View("~/Modules/Order/BayiOrders/BayiOrdersIndex.cshtml", model);
    }
}
