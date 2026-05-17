using MyRow = SYP.Catalog.ProductsRow;

namespace SYP.Catalog;

public interface IProductsRetrieveHandler : IRetrieveHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>> { }

public class ProductsRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>>(context),
    IProductsRetrieveHandler
{
    protected override void OnReturn()
    {
        base.OnReturn();

        // Ürün için güncel fiyatı hesapla
        if (Row.Id == null)
            return;

        var today = DateTime.Today;
        var fld = PriceListItemsRow.Fields;

        // Ürünün fiyat listesi kalemlerini getir
        var priceListItems = Connection.List<PriceListItemsRow>(q => q
            .Select(fld.Id)
            .Select(fld.ProductId)
            .Select(fld.PriceListId)
            .Select(fld.UnitPrice)
            .Select(fld.PriceListValidFrom)
            .Select(fld.PriceListValidTo)
            .Select(fld.PriceListIsActive)
            .Select(fld.PriceListIsDefault)
            .Where(fld.ProductId == Row.Id.Value)
        );

        // Aktif ve geçerli tarih aralığındaki kalemleri filtrele
        var validItems = priceListItems.Where(item =>
        {
            if (item.PriceListIsActive != true)
                return false;

            if (item.PriceListValidFrom.HasValue && item.PriceListValidFrom.Value.Date > today)
                return false;

            if (item.PriceListValidTo.HasValue && item.PriceListValidTo.Value.Date < today)
                return false;

            return true;
        }).ToList();

        if (validItems.Count == 0)
            return;

        // Öncelik: IsDefault -> Aynı listede en son kayıt -> ValidFrom en yeni
        var validPrice = validItems
            .OrderByDescending(x => x.PriceListIsDefault == true ? 1 : 0)
            .ThenByDescending(x => x.Id)
            .FirstOrDefault();

        if (validPrice != null)
        {
            Row.CurrentValidPrice = validPrice.UnitPrice;
        }
    }
}