using MyRow = SYP.Catalog.ProductPackingRow;

namespace SYP.Catalog;

public interface IProductPackingListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class ProductPackingListHandler(IRequestContext context) :
    ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>(context),
    IProductPackingListHandler
{
}
