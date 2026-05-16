using MyRow = SYP.Products.BrandsRow;

namespace SYP.Products;

public interface IBrandsDeleteHandler : IDeleteHandler<MyRow, DeleteRequest, DeleteResponse> { }

public class BrandsDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<MyRow, DeleteRequest, DeleteResponse>(context),
    IBrandsDeleteHandler
{
}