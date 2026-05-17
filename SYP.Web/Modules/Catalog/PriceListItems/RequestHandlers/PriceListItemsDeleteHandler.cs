using MyRow = SYP.Catalog.PriceListItemsRow;

namespace SYP.Catalog;

public interface IPriceListItemsDeleteHandler : IDeleteHandler<MyRow, DeleteRequest, DeleteResponse> { }

public class PriceListItemsDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<MyRow, DeleteRequest, DeleteResponse>(context),
    IPriceListItemsDeleteHandler
{
}
