using MyRow = SYP.Setting.VatRatesRow;

namespace SYP.Setting;

public interface IVatRatesDeleteHandler : IDeleteHandler<MyRow, DeleteRequest, DeleteResponse> { }

public class VatRatesDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<MyRow, DeleteRequest, DeleteResponse>(context),
    IVatRatesDeleteHandler
{
}