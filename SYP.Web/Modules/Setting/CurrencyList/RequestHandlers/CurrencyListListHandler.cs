using MyRow = SYP.Setting.CurrencyListRow;

namespace SYP.Setting;

public interface ICurrencyListListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class CurrencyListListHandler(IRequestContext context) :
    ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>(context),
    ICurrencyListListHandler
{
}