using MyRow = SYP.Order.OrderRow;

namespace SYP.Order;

public interface IOrderDeleteHandler : IDeleteHandler<MyRow, DeleteRequest, DeleteResponse> { }

public class OrderDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<MyRow, DeleteRequest, DeleteResponse>(context),
    IOrderDeleteHandler
{
}