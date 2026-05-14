using MyRow = SYP.Setting.CurrencyListRow;

namespace SYP.Setting;

public interface ICurrencyListRetrieveHandler : IRetrieveHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>> { }

public class CurrencyListRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>>(context),
    ICurrencyListRetrieveHandler
{
}