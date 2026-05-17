using MyRow = SYP.Catalog.ProductCategoryRow;

namespace SYP.Catalog.RequestHandlers;

public interface IProductCategoryRetrieveHandler : IRetrieveHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>> { }

public class ProductCategoryRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>>(context),
    IProductCategoryRetrieveHandler
{
}