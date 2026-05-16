using MyRow = SYP.Products.ProductCategoryRow;

namespace SYP.Products;

public interface IProductCategoryDeleteHandler : IDeleteHandler<MyRow, DeleteRequest, DeleteResponse> { }

public class ProductCategoryDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<MyRow, DeleteRequest, DeleteResponse>(context),
    IProductCategoryDeleteHandler
{
}