using MyRow = SYP.Order.OrderStatusHistRow;

namespace SYP.Order;

public interface IOrderStatusHistDeleteHandler : IDeleteHandler<MyRow, DeleteRequest, DeleteResponse> { }

public class OrderStatusHistDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<MyRow, DeleteRequest, DeleteResponse>(context),
    IOrderStatusHistDeleteHandler
{
}