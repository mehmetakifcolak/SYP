using Serenity.Services;
using MyRow = SYP.Warehouse.StockEntriesRow;

namespace SYP.Warehouse;

public interface IStockEntriesListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class StockEntriesListHandler : ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>, IStockEntriesListHandler
{
    public StockEntriesListHandler(IRequestContext context)
        : base(context)
    {
    }
}
