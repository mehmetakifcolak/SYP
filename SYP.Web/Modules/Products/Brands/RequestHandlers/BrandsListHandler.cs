using MyRow = SYP.Products.BrandsRow;

namespace SYP.Products;

public interface IBrandsListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class BrandsListHandler(IRequestContext context) :
    ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>(context),
    IBrandsListHandler
{
}