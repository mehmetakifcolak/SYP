using MyRow = SYP.Setting.CountryRow;

namespace SYP.Setting;

public interface ICountryDeleteHandler : IDeleteHandler<MyRow, DeleteRequest, DeleteResponse> { }

public class CountryDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<MyRow, DeleteRequest, DeleteResponse>(context),
    ICountryDeleteHandler
{
}