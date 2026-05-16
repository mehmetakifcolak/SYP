using MyRow = SYP.Setting.CountryRow;

namespace SYP.Setting;

public interface ICountryRetrieveHandler : IRetrieveHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>> { }

public class CountryRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>>(context),
    ICountryRetrieveHandler
{
}