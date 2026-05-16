using Serenity.Services;
using MyRow = SYP.Catalog.PriceListItemsRow;

namespace SYP.Catalog;

public interface IPriceListItemsRetrieveHandler : IRetrieveHandler<MyRow> { }

public class PriceListItemsRetrieveHandler : RetrieveRequestHandler<MyRow>, IPriceListItemsRetrieveHandler
{
    public PriceListItemsRetrieveHandler(IRequestContext context) : base(context) { }
}
