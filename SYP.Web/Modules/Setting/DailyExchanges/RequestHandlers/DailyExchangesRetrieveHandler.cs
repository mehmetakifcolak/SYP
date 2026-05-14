using MyRow = SYP.Setting.DailyExchangesRow;

namespace SYP.Setting;

public interface IDailyExchangesRetrieveHandler : IRetrieveHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>> { }

public class DailyExchangesRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>>(context),
    IDailyExchangesRetrieveHandler
{
}