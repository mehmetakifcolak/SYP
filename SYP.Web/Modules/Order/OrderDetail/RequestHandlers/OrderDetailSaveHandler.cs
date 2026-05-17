using MyRow = SYP.Order.OrderDetailRow;

namespace SYP.Order;

public interface IOrderDetailSaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class OrderDetailSaveHandler(IRequestContext context) :
    SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>(context),
    IOrderDetailSaveHandler
{
}