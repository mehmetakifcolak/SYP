using MyRow = SYP.Catalog.ProductCategoryRow;

namespace SYP.Catalog.RequestHandlers;

public interface IProductCategorySaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class ProductCategorySaveHandler(IRequestContext context) :
    SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>(context),
    IProductCategorySaveHandler
{
}