using Serenity.Services;
using MyRow = SYP.Warehouse.StockEntriesRow;

namespace SYP.Warehouse;

public interface IStockEntriesRetrieveHandler : IRetrieveHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>> { }

public class StockEntriesRetrieveHandler : RetrieveRequestHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>>, IStockEntriesRetrieveHandler
{
    public StockEntriesRetrieveHandler(IRequestContext context)
        : base(context)
    {
    }
}
