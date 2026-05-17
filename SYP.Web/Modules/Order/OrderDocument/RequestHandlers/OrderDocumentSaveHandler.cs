using MyRow = SYP.Order.OrderDocumentRow;

namespace SYP.Order;

public interface IOrderDocumentSaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class OrderDocumentSaveHandler(IRequestContext context) :
    SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>(context),
    IOrderDocumentSaveHandler
{
}