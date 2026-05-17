using MyRow = SYP.Catalog.BrandsRow;

namespace SYP.Catalog;

public interface IBrandsDeleteHandler : IDeleteHandler<MyRow, DeleteRequest, DeleteResponse> { }

public class BrandsDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<MyRow, DeleteRequest, DeleteResponse>(context),
    IBrandsDeleteHandler
{
}