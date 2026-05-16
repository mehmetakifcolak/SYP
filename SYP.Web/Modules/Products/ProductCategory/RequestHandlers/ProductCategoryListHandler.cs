using MyRow = SYP.Products.ProductCategoryRow;

namespace SYP.Products;

public interface IProductCategoryListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class ProductCategoryListHandler(IRequestContext context) :
    ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>(context),
    IProductCategoryListHandler
{
}