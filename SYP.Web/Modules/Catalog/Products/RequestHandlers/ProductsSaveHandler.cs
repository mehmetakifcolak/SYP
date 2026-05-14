using MyRow = SYP.Catalog.ProductsRow;

namespace SYP.Catalog;

public interface IProductsSaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class ProductsSaveHandler(IRequestContext context) :
    SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>(context),
    IProductsSaveHandler
{
}