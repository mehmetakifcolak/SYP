using MyRow = SYP.Catalog.PriceListItemsRow;

namespace SYP.Catalog;

public interface IPriceListItemsSaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class PriceListItemsSaveHandler(IRequestContext context) :
    SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>(context),
    IPriceListItemsSaveHandler
{
    protected override void ValidateRequest()
    {
        base.ValidateRequest();

        var row = Row;

        // UnitPrice > 0 kontrolü
        if (row.UnitPrice <= 0)
        {
            throw new ValidationError("UnitPrice", "Birim fiyat 0'dan büyük olmalıdır!");
        }

        // DiscountRate 0-100 aralığında
        if (row.DiscountRate.HasValue && (row.DiscountRate < 0 || row.DiscountRate > 100))
        {
            throw new ValidationError("DiscountRate", "İndirim oranı 0-100 arasında olmalıdır!");
        }

        // Duplicate kontrolü kaldırıldı - aynı ürün farklı fiyatlarla eklenebilir
        // Kullanıcı fiyat güncellemesi yapabilir ve geçmiş fiyatları takip edebilir
    }
}
