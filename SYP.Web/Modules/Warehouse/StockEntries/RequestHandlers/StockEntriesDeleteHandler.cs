using Serenity.Services;
using MyRow = SYP.Warehouse.StockEntriesRow;

namespace SYP.Warehouse;

public interface IStockEntriesDeleteHandler : IDeleteHandler<MyRow, DeleteRequest, DeleteResponse> { }

public class StockEntriesDeleteHandler : DeleteRequestHandler<MyRow, DeleteRequest, DeleteResponse>, IStockEntriesDeleteHandler
{
    public StockEntriesDeleteHandler(IRequestContext context)
        : base(context)
    {
    }

    protected override void OnBeforeDelete()
    {
        base.OnBeforeDelete();

        // Onaylanmış kayıtlar silinemez
        if (Row.Status == StockEntryStatus.Approved)
        {
            throw new ValidationError("Onaylanmış stok girişleri silinemez!");
        }
    }
}
