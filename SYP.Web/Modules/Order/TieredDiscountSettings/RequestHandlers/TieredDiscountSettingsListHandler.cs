using MyRow = SYP.Order.TieredDiscountSettingsRow;

namespace SYP.Order;

public interface ITieredDiscountSettingsListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class TieredDiscountSettingsListHandler(IRequestContext context) :
    ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>(context),
    ITieredDiscountSettingsListHandler
{
}