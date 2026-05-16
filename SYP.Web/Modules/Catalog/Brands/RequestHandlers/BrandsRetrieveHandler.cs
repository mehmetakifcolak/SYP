using Serenity.Services;

namespace SYP.Catalog;

public interface IBrandsRetrieveHandler : IRetrieveHandler<BrandsRow> { }

public class BrandsRetrieveHandler : RetrieveRequestHandler<BrandsRow>, IBrandsRetrieveHandler
{
    public BrandsRetrieveHandler(IRequestContext context) : base(context) { }
}
