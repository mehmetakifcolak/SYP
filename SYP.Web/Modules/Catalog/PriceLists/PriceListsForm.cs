using Serenity.ComponentModel;

namespace SYP.Catalog.Forms;

[FormScript("Catalog.PriceLists")]
[BasedOnRow(typeof(PriceListsRow), CheckNames = true)]
public class PriceListsForm
{
    [Tab("Genel Bilgiler")]
    [Category("Fiyat Listesi Bilgileri")]
    public string Code { get; set; }
    public string Name { get; set; }
    public int CurrencyId { get; set; }

    [HalfWidth]
    public DateTime ValidFrom { get; set; }
    [HalfWidth]
    public DateTime ValidTo { get; set; }

    [HalfWidth]
    public short IsActive { get; set; }
    [HalfWidth]
    public bool IsDefault { get; set; }

    [TextAreaEditor(Rows = 3)]
    public string Description { get; set; }

    [Tab("Ürün Fiyatları")]
    [Category("Fiyat Listesi Kalemleri")]
    [PriceListItemsEditor]
    public List<PriceListItemsRow> ItemList { get; set; }
}
