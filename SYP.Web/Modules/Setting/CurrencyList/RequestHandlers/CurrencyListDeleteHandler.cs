using MyRow = SYP.Setting.CurrencyListRow;

namespace SYP.Setting;

public interface ICurrencyListDeleteHandler : IDeleteHandler<MyRow, DeleteRequest, DeleteResponse> { }

public class CurrencyListDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<MyRow, DeleteRequest, DeleteResponse>(context),
    ICurrencyListDeleteHandler
{
}