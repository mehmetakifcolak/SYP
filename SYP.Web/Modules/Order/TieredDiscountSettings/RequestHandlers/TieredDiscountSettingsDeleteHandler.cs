using MyRow = SYP.Order.TieredDiscountSettingsRow;

namespace SYP.Order;

public interface ITieredDiscountSettingsDeleteHandler : IDeleteHandler<MyRow, DeleteRequest, DeleteResponse> { }

public class TieredDiscountSettingsDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<MyRow, DeleteRequest, DeleteResponse>(context),
    ITieredDiscountSettingsDeleteHandler
{
}