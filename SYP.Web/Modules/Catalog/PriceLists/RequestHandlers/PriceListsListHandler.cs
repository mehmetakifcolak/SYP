using Serenity.Services;
using MyRow = SYP.Catalog.PriceListsRow;

namespace SYP.Catalog;

public interface IPriceListsListHandler : IListHandler<MyRow> { }

public class PriceListsListHandler : ListRequestHandler<MyRow>, IPriceListsListHandler
{
    public PriceListsListHandler(IRequestContext context) : base(context) { }
}
