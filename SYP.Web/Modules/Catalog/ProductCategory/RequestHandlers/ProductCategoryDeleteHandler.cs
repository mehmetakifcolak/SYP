using MyRow = SYP.Catalog.ProductCategoryRow;

namespace SYP.Catalog.RequestHandlers;

public interface IProductCategoryDeleteHandler : IDeleteHandler<MyRow, DeleteRequest, DeleteResponse> { }

public class ProductCategoryDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<MyRow, DeleteRequest, DeleteResponse>(context),
    IProductCategoryDeleteHandler
{
}