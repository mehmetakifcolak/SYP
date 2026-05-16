using Serenity.Services;

namespace SYP.Catalog;

public interface IBrandsListHandler : IListHandler<BrandsRow> { }

public class BrandsListHandler : ListRequestHandler<BrandsRow>, IBrandsListHandler
{
    public BrandsListHandler(IRequestContext context) : base(context) { }
}
