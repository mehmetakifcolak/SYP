using MyRow = SYP.Setting.DailyExchangesRow;

namespace SYP.Setting;

public interface IDailyExchangesSaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class DailyExchangesSaveHandler(IRequestContext context) :
    SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>(context),
    IDailyExchangesSaveHandler
{
}