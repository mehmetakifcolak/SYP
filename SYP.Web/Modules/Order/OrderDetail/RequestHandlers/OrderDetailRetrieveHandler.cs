using MyRow = SYP.Order.OrderDetailRow;

namespace SYP.Order;

public interface IOrderDetailRetrieveHandler : IRetrieveHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>> { }

public class OrderDetailRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>>(context),
    IOrderDetailRetrieveHandler
{
}