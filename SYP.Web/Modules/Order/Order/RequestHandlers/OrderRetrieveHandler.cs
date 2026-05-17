using MyRow = SYP.Order.OrderRow;

namespace SYP.Order;

public interface IOrderRetrieveHandler : IRetrieveHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>> { }

public class OrderRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>>(context),
    IOrderRetrieveHandler
{
}