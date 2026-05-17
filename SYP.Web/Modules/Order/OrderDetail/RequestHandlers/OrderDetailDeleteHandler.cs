using MyRow = SYP.Order.OrderDetailRow;

namespace SYP.Order;

public interface IOrderDetailDeleteHandler : IDeleteHandler<MyRow, DeleteRequest, DeleteResponse> { }

public class OrderDetailDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<MyRow, DeleteRequest, DeleteResponse>(context),
    IOrderDetailDeleteHandler
{
}