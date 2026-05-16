using MyRow = SYP.Products.BrandsRow;

namespace SYP.Products;

public interface IBrandsSaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class BrandsSaveHandler(IRequestContext context) :
    SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>(context),
    IBrandsSaveHandler
{
}