using MyRow = SYP.Setting.CountryRow;

namespace SYP.Setting;

public interface ICountrySaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class CountrySaveHandler(IRequestContext context) :
    SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>(context),
    ICountrySaveHandler
{
}