using MyRow = SYP.Catalog.ProductPackingRow;

namespace SYP.Catalog;

public interface IProductPackingRetrieveHandler : IRetrieveHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>> { }

public class ProductPackingRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>>(context),
    IProductPackingRetrieveHandler
{
}
