using Serenity.Services;
using MyRow = SYP.Warehouse.StockExitsRow;

namespace SYP.Warehouse;

public interface IStockExitsRetrieveHandler : IRetrieveHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>> { }

public class StockExitsRetrieveHandler : RetrieveRequestHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>>, IStockExitsRetrieveHandler
{
    public StockExitsRetrieveHandler(IRequestContext context)
        : base(context)
    {
    }
}
