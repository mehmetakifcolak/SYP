using MyRow = SYP.Setting.VendorTypeRow;

namespace SYP.Setting;

public interface IVendorTypeDeleteHandler : IDeleteHandler<MyRow, DeleteRequest, DeleteResponse> { }

public class VendorTypeDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<MyRow, DeleteRequest, DeleteResponse>(context),
    IVendorTypeDeleteHandler
{
}