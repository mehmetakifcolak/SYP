using Serenity.Services;

namespace SYP.Catalog;

public interface IBrandsSaveHandler : ISaveHandler<BrandsRow> { }

public class BrandsSaveHandler : SaveRequestHandler<BrandsRow>, IBrandsSaveHandler
{
    public BrandsSaveHandler(IRequestContext context) : base(context) { }
}
