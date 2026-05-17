using System.Data;

namespace SYP.Order;

public interface IOrderStatusService
{
    void LogStatusChange(IDbConnection connection, int orderId, OrderStatus? oldStatus, OrderStatus? newStatus,
        int userId, string userRole, string reason = null);
}
