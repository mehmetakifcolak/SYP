using MyRow = SYP.Products.ProductCategoryRow;

namespace SYP.Products;

public interface IProductCategorySaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class ProductCategorySaveHandler(IRequestContext context) :
    SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>(context),
    IProductCategorySaveHandler
{
}