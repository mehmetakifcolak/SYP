using MyRow = SYP.Order.OrderDetailRow;

namespace SYP.Order;

public interface IOrderDetailListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class OrderDetailListHandler(IRequestContext context) :
    ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>(context),
    IOrderDetailListHandler
{
}