using MyRow = SYP.Order.OrderStatusHistRow;

namespace SYP.Order;

public interface IOrderStatusHistRetrieveHandler : IRetrieveHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>> { }

public class OrderStatusHistRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>>(context),
    IOrderStatusHistRetrieveHandler
{
}