using Serenity.Data;
using Serenity.Services;
using System.Collections.Generic;
using System.Linq;
using MyRow = SYP.Catalog.PriceListsRow;

namespace SYP.Catalog;

public interface IPriceListsRetrieveHandler : IRetrieveHandler<MyRow> { }

public class PriceListsRetrieveHandler : RetrieveRequestHandler<MyRow>, IPriceListsRetrieveHandler
{
    public PriceListsRetrieveHandler(IRequestContext context) : base(context) { }

    protected override void OnReturn()
    {
        base.OnReturn();

        if (Row != null && Row.Id != null)
        {
            var itemFld = PriceListItemsRow.Fields;
            var items = Connection.List<PriceListItemsRow>(q => q
                .SelectTableFields()
                .Select(itemFld.ProductCode)
                .Select(itemFld.ProductName)
                .Where(itemFld.PriceListId == Row.Id.Value)
                .OrderBy(itemFld.ProductCode));

            // Response'a ItemList ekle
            Response.Entity.ItemList = items;
        }
    }
}
