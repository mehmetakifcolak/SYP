using Serenity.Data;
using System;
using System.Data;
using _Ext;

namespace SYP.Order;

public class OrderStatusService : IOrderStatusService
{
    public void LogStatusChange(IDbConnection connection, int orderId, OrderStatus? oldStatus, OrderStatus? newStatus,
        int userId, string userRole, string reason = null)
    {
        var row = new OrderStatusHistRow
        {
            OrderId = orderId,
            OldStatus = (int?)oldStatus,
            NewStatus = (int?)newStatus,
            ChangedByUserId = userId,
            ChangedByUserRole = userRole,
            ChangeReason = reason,
            ChangeDate = DateTime.Now
        };

        connection.Insert(row);
    }
}
