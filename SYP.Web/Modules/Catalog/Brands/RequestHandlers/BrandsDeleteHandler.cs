using Serenity.Services;

namespace SYP.Catalog;

public interface IBrandsDeleteHandler : IDeleteHandler<BrandsRow> { }

public class BrandsDeleteHandler : DeleteRequestHandler<BrandsRow>, IBrandsDeleteHandler
{
    public BrandsDeleteHandler(IRequestContext context) : base(context) { }
}
