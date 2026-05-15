using Serenity.Services;
using MyRow = SYP.Warehouse.WarehouseStockRow;

namespace SYP.Warehouse;

public interface IWarehouseStockListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class WarehouseStockListHandler : ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>, IWarehouseStockListHandler
{
    public WarehouseStockListHandler(IRequestContext context)
        : base(context)
    {
    }
}
