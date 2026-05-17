using MyRow = SYP.Catalog.ProductCategoryRow;

namespace SYP.Catalog.RequestHandlers;

public interface IProductCategoryListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class ProductCategoryListHandler(IRequestContext context) :
    ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>(context),
    IProductCategoryListHandler
{
}