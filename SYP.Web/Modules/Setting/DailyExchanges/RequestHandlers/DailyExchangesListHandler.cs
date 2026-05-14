using MyRow = SYP.Setting.DailyExchangesRow;

namespace SYP.Setting;

public interface IDailyExchangesListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class DailyExchangesListHandler(IRequestContext context) :
    ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>(context),
    IDailyExchangesListHandler
{
}