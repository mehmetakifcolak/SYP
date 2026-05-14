using MyRow = SYP.Setting.VatRatesRow;

namespace SYP.Setting;

public interface IVatRatesRetrieveHandler : IRetrieveHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>> { }

public class VatRatesRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>>(context),
    IVatRatesRetrieveHandler
{
}