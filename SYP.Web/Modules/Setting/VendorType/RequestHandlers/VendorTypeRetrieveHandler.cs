using MyRow = SYP.Setting.VendorTypeRow;

namespace SYP.Setting;

public interface IVendorTypeRetrieveHandler : IRetrieveHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>> { }

public class VendorTypeRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>>(context),
    IVendorTypeRetrieveHandler
{
}