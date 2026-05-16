using Serenity.Services;
using MyRow = SYP.Warehouse.StockExitsRow;

namespace SYP.Warehouse;

public interface IStockExitsListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class StockExitsListHandler : ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>, IStockExitsListHandler
{
    public StockExitsListHandler(IRequestContext context)
        : base(context)
    {
    }
}
