using MyRow = SYP.Order.TieredDiscountSettingsRow;

namespace SYP.Order;

public interface ITieredDiscountSettingsSaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class TieredDiscountSettingsSaveHandler(IRequestContext context) :
    SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>(context),
    ITieredDiscountSettingsSaveHandler
{
}