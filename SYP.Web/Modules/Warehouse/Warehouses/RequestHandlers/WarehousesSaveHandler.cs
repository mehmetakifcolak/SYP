using Serenity.Data;
using Serenity.Services;
using SYP.Administration;

namespace SYP.Warehouse;

public interface IWarehousesSaveHandler : ISaveHandler<WarehousesRow, SaveRequest<WarehousesRow>, SaveResponse> { }

public class WarehousesSaveHandler(IRequestContext context) :
    SaveRequestHandler<WarehousesRow, SaveRequest<WarehousesRow>, SaveResponse>(context),
    IWarehousesSaveHandler
{
    protected override void BeforeSave()
    {
        base.BeforeSave();

        var currentUserId = UserHelper.GetCurrentUserId(Connection, Context.User?.GetIdentifier());

        if (IsCreate)
        {
            Row.InsertDate = DateTime.Now;
            Row.InsertUserId = currentUserId;
        }
        else
        {
            Row.UpdateDate = DateTime.Now;
            Row.UpdateUserId = currentUserId;
        }
    }

    protected override void AfterSave()
    {
        base.AfterSave();

        // Eğer bu depo varsayılan yapıldıysa, diğer depoların varsayılan özelliğini kaldır
        if (Row.IsDefault == true)
        {
            Connection.Execute($@"
                UPDATE Warehouses
                SET IsDefault = 0
                WHERE Id <> @id AND IsDefault = 1",
                new { id = Row.Id });
        }
    }
}
