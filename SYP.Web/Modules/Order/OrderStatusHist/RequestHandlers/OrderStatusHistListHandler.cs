using MyRow = SYP.Order.OrderStatusHistRow;

namespace SYP.Order;

public interface IOrderStatusHistListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class OrderStatusHistListHandler(IRequestContext context) :
    ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>(context),
    IOrderStatusHistListHandler
{
}