using Serenity.Services;
using MyRow = SYP.Catalog.PriceListsRow;

namespace SYP.Catalog;

public interface IPriceListsDeleteHandler : IDeleteHandler<MyRow> { }

public class PriceListsDeleteHandler : DeleteRequestHandler<MyRow>, IPriceListsDeleteHandler
{
    public PriceListsDeleteHandler(IRequestContext context) : base(context) { }

    protected override void OnBeforeDelete()
    {
        base.OnBeforeDelete();

        // Fiyat listesi kalemlerini sil
        new SqlDelete(PriceListItemsRow.Fields.TableName)
            .Where(PriceListItemsRow.Fields.PriceListId == Row.Id.Value)
            .Execute(Connection, ExpectedRows.Ignore);
    }
}
