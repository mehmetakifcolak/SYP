using Serenity.Services;
using MyRow = SYP.Warehouse.StockMovementsRow;

namespace SYP.Warehouse;

public interface IStockMovementsRetrieveHandler : IRetrieveHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>> { }

public class StockMovementsRetrieveHandler : RetrieveRequestHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>>, IStockMovementsRetrieveHandler
{
    public StockMovementsRetrieveHandler(IRequestContext context)
        : base(context)
    {
    }
}
