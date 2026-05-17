using MyRow = SYP.Catalog.PriceListsRow;

namespace SYP.Catalog;

public interface IPriceListsDeleteHandler : IDeleteHandler<MyRow, DeleteRequest, DeleteResponse> { }

public class PriceListsDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<MyRow, DeleteRequest, DeleteResponse>(context),
    IPriceListsDeleteHandler
{
}