using MyRow = SYP.Catalog.BrandsRow;

namespace SYP.Catalog;

public interface IBrandsListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class BrandsListHandler(IRequestContext context) :
    ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>(context),
    IBrandsListHandler
{
}