using MyRow = SYP.Setting.VendorTypeRow;

namespace SYP.Setting;

public interface IVendorTypeListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class VendorTypeListHandler(IRequestContext context) :
    ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>(context),
    IVendorTypeListHandler
{
}