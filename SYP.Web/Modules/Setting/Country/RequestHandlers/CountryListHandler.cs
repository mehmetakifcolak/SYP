using MyRow = SYP.Setting.CountryRow;

namespace SYP.Setting;

public interface ICountryListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class CountryListHandler(IRequestContext context) :
    ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>(context),
    ICountryListHandler
{
}