using MyRow = SYP.Setting.DailyExchangesRow;

namespace SYP.Setting;

public interface IDailyExchangesDeleteHandler : IDeleteHandler<MyRow, DeleteRequest, DeleteResponse> { }

public class DailyExchangesDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<MyRow, DeleteRequest, DeleteResponse>(context),
    IDailyExchangesDeleteHandler
{
}