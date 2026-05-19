using MyRow = SYP.Catalog.ProductPackingRow;

namespace SYP.Catalog;

public interface IProductPackingDeleteHandler : IDeleteHandler<MyRow, DeleteRequest, DeleteResponse> { }

public class ProductPackingDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<MyRow, DeleteRequest, DeleteResponse>(context),
    IProductPackingDeleteHandler
{
}
