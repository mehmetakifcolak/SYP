using MyRow = SYP.Order.OrderDocumentRow;

namespace SYP.Order;

public interface IOrderDocumentDeleteHandler : IDeleteHandler<MyRow, DeleteRequest, DeleteResponse> { }

public class OrderDocumentDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<MyRow, DeleteRequest, DeleteResponse>(context),
    IOrderDocumentDeleteHandler
{
}