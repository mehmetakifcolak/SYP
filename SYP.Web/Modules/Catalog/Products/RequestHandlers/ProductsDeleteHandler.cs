using MyRow = SYP.Catalog.ProductsRow;

namespace SYP.Catalog;

public interface IProductsDeleteHandler : IDeleteHandler<MyRow, DeleteRequest, DeleteResponse> { }

public class ProductsDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<MyRow, DeleteRequest, DeleteResponse>(context),
    IProductsDeleteHandler
{
}