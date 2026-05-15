using Serenity.Services;

namespace SYP.Warehouse;

public interface IWarehousesDeleteHandler : IDeleteHandler<WarehousesRow, DeleteRequest, DeleteResponse> { }

public class WarehousesDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<WarehousesRow, DeleteRequest, DeleteResponse>(context),
    IWarehousesDeleteHandler
{
}
