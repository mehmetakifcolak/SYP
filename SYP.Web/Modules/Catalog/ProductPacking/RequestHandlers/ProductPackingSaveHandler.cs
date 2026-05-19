using MyRow = SYP.Catalog.ProductPackingRow;

namespace SYP.Catalog;

public interface IProductPackingSaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class ProductPackingSaveHandler(IRequestContext context) :
    SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>(context),
    IProductPackingSaveHandler
{
}
