using MyRow = SYP.Order.OrderDocumentRow;

namespace SYP.Order;

public interface IOrderDocumentRetrieveHandler : IRetrieveHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>> { }

public class OrderDocumentRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>>(context),
    IOrderDocumentRetrieveHandler
{
}