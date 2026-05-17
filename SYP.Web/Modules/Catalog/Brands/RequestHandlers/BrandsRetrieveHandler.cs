using MyRow = SYP.Catalog.BrandsRow;

namespace SYP.Catalog;

public interface IBrandsRetrieveHandler : IRetrieveHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>> { }

public class BrandsRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>>(context),
    IBrandsRetrieveHandler
{
}