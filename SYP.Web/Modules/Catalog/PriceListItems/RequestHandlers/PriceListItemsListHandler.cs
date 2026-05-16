using Serenity.Services;
using MyRow = SYP.Catalog.PriceListItemsRow;

namespace SYP.Catalog;

public interface IPriceListItemsListHandler : IListHandler<MyRow> { }

public class PriceListItemsListHandler : ListRequestHandler<MyRow>, IPriceListItemsListHandler
{
    public PriceListItemsListHandler(IRequestContext context) : base(context) { }
}
