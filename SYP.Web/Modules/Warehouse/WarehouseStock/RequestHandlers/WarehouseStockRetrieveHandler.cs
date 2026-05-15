using Serenity.Services;
using MyRow = SYP.Warehouse.WarehouseStockRow;

namespace SYP.Warehouse;

public interface IWarehouseStockRetrieveHandler : IRetrieveHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>> { }

public class WarehouseStockRetrieveHandler : RetrieveRequestHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>>, IWarehouseStockRetrieveHandler
{
    public WarehouseStockRetrieveHandler(IRequestContext context)
        : base(context)
    {
    }
}
