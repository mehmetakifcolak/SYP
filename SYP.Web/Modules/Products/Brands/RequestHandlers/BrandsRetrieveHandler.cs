using MyRow = SYP.Products.BrandsRow;

namespace SYP.Products;

public interface IBrandsRetrieveHandler : IRetrieveHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>> { }

public class BrandsRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>>(context),
    IBrandsRetrieveHandler
{
}