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
}
