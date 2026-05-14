using MyRow = SYP.Setting.CurrencyListRow;

namespace SYP.Setting;

public interface ICurrencyListSaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class CurrencyListSaveHandler(IRequestContext context) :
    SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>(context),
    ICurrencyListSaveHandler
{
}