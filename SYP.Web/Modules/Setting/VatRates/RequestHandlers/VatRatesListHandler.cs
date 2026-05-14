using MyRow = SYP.Setting.VatRatesRow;

namespace SYP.Setting;

public interface IVatRatesListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class VatRatesListHandler(IRequestContext context) :
    ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>(context),
    IVatRatesListHandler
{
}