using MyRow = SYP.Setting.VatRatesRow;

namespace SYP.Setting;

public interface IVatRatesSaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class VatRatesSaveHandler(IRequestContext context) :
    SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>(context),
    IVatRatesSaveHandler
{
}