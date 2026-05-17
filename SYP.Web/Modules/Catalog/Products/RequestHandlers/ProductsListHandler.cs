using MyRow = SYP.Catalog.ProductsRow;

namespace SYP.Catalog;

public interface IProductsListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class ProductsListHandler(IRequestContext context) :
    ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>(context),
    IProductsListHandler
{
    protected override void OnReturn()
    {
        base.OnReturn();

        // Tüm ürünler için güncel fiyatları hesapla
        var productIds = Response.Entities.Select(p => p.Id.Value).ToList();
        if (productIds.Count == 0)
            return;

        var today = DateTime.Today;
        var priceListItemsFld = PriceListItemsRow.Fields;

        // Tüm ürünlerin fiyat listesi kalemlerini getir
        var priceListItems = Connection.List<PriceListItemsRow>(q => q
            .Select(priceListItemsFld.Id)
            .Select(priceListItemsFld.ProductId)
            .Select(priceListItemsFld.PriceListId)
            .Select(priceListItemsFld.UnitPrice)
            .Select(priceListItemsFld.PriceListValidFrom)
            .Select(priceListItemsFld.PriceListValidTo)
            .Select(priceListItemsFld.PriceListIsActive)
            .Select(priceListItemsFld.PriceListIsDefault)
            .Where(new Criteria(priceListItemsFld.ProductId).In(productIds))
        );

        // Ürün bazında grupla
        var pricesByProduct = priceListItems
            .GroupBy(item => item.ProductId.Value)
            .ToDictionary(g => g.Key, g => g.ToList());

        // Her ürün için güncel fiyatı hesapla
        foreach (var product in Response.Entities)
        {
            if (!pricesByProduct.TryGetValue(product.Id.Value, out var items))
                continue;

            // Aktif ve geçerli tarih aralığındaki kalemleri filtrele
            var validItems = items.Where(item =>
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
                continue;

            // Öncelik: IsDefault -> Aynı listede en son kayıt -> ValidFrom en yeni
            var validPrice = validItems
                .OrderByDescending(x => x.PriceListIsDefault == true ? 1 : 0)
                .ThenByDescending(x => x.Id)
                .FirstOrDefault();

            if (validPrice != null)
            {
                product.CurrentValidPrice = validPrice.UnitPrice;
            }
        }
    }
}