using MyRow = SYP.Catalog.PriceListsRow;

namespace SYP.Catalog;

public interface IPriceListsListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class PriceListsListHandler(IRequestContext context) :
    ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>(context),
    IPriceListsListHandler
{
}