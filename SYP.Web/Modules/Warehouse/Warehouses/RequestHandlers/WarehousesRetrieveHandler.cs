using Serenity.Services;

namespace SYP.Warehouse;

public interface IWarehousesRetrieveHandler : IRetrieveHandler<WarehousesRow, RetrieveRequest, RetrieveResponse<WarehousesRow>> { }

public class WarehousesRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<WarehousesRow, RetrieveRequest, RetrieveResponse<WarehousesRow>>(context),
    IWarehousesRetrieveHandler
{
}
