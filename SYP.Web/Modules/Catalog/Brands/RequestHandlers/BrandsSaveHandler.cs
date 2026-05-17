using MyRow = SYP.Catalog.BrandsRow;

namespace SYP.Catalog;

public interface IBrandsSaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class BrandsSaveHandler(IRequestContext context) :
    SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>(context),
    IBrandsSaveHandler
{
}