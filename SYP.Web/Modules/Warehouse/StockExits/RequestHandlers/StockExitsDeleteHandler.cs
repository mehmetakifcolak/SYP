using Serenity.Services;
using _Ext;
using MyRow = SYP.Warehouse.StockExitsRow;

namespace SYP.Warehouse;

public interface IStockExitsDeleteHandler : IDeleteHandler<MyRow, DeleteRequest, DeleteResponse> { }

public class StockExitsDeleteHandler : DeleteRequestHandler<MyRow, DeleteRequest, DeleteResponse>, IStockExitsDeleteHandler
{
    public StockExitsDeleteHandler(IRequestContext context)
        : base(context)
    {
    }

    protected override void OnBeforeDelete()
    {
        base.OnBeforeDelete();

        // Onaylanmış kayıtlar silinemez
        if (Row.Status == StockExitStatus.Approved)
        {
            throw new ValidationError("Onaylanmış stok çıkışları silinemez!");
        }
    }
}
