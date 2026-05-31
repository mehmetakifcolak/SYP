using MyRow = SYP.Catalog.ProductsRow;

namespace SYP.Catalog;

public interface IProductsRetrieveHandler : IRetrieveHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>> { }

public class ProductsRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>>(context),
    IProductsRetrieveHandler
{
}