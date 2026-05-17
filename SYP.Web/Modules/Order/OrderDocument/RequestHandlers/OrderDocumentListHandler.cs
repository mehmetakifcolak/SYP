using MyRow = SYP.Order.OrderDocumentRow;

namespace SYP.Order;

public interface IOrderDocumentListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class OrderDocumentListHandler(IRequestContext context) :
    ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>(context),
    IOrderDocumentListHandler
{
}