using MyRow = SYP.Products.ProductCategoryRow;

namespace SYP.Products;

public interface IProductCategoryRetrieveHandler : IRetrieveHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>> { }

public class ProductCategoryRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>>(context),
    IProductCategoryRetrieveHandler
{
}