using MyRow = SYP.Order.OrderStatusHistRow;

namespace SYP.Order;

public interface IOrderStatusHistSaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class OrderStatusHistSaveHandler(IRequestContext context) :
    SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>(context),
    IOrderStatusHistSaveHandler
{
}