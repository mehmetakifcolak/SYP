using Serenity.Services;
using MyRow = SYP.Warehouse.StockMovementsRow;

namespace SYP.Warehouse;

public interface IStockMovementsListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class StockMovementsListHandler : ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>, IStockMovementsListHandler
{
    public StockMovementsListHandler(IRequestContext context)
        : base(context)
    {
    }
}
