using MyRow = SYP.Setting.VendorTypeRow;

namespace SYP.Setting;

public interface IVendorTypeSaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class VendorTypeSaveHandler(IRequestContext context) :
    SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>(context),
    IVendorTypeSaveHandler
{
}