using MyRow = SYP.Order.TieredDiscountSettingsRow;

namespace SYP.Order;

public interface ITieredDiscountSettingsRetrieveHandler : IRetrieveHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>> { }

public class TieredDiscountSettingsRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>>(context),
    ITieredDiscountSettingsRetrieveHandler
{
}