using Serenity.Services;

namespace SYP.Warehouse;

public interface IWarehousesListHandler : IListHandler<WarehousesRow, ListRequest, ListResponse<WarehousesRow>> { }

public class WarehousesListHandler(IRequestContext context) :
    ListRequestHandler<WarehousesRow, ListRequest, ListResponse<WarehousesRow>>(context),
    IWarehousesListHandler
{
}
