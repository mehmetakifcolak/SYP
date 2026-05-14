using MyRow = SYP.Catalog.ProductsRow;

namespace SYP.Catalog;

public interface IProductsListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class ProductsListHandler(IRequestContext context) :
    ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>(context),
    IProductsListHandler
{
}