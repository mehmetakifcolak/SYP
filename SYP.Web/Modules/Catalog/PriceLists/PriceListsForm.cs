using Serenity.ComponentModel;

namespace SYP.Catalog.Forms;

[FormScript("Catalog.PriceLists")]
[BasedOnRow(typeof(PriceListsRow), CheckNames = true)]
public class PriceListsForm
{
    [Tab("General Information")]
    [HalfWidth, Placeholder("Leave blank to auto-generate")]
    public string Code { get; set; }

    [HalfWidth]
    public int CurrencyId { get; set; }

    [FullWidth]
    public string Name { get; set; }

    [FullWidth, TextAreaEditor(Rows = 3)]
    public string Description { get; set; }

    [HalfWidth, DateEditor]
    public DateTime ValidFrom { get; set; }

    [HalfWidth, DateEditor]
    public DateTime ValidTo { get; set; }

    [HalfWidth, DefaultValue(true)]
    public bool IsActive { get; set; }

    [HalfWidth]
    public bool IsDefault { get; set; }

    [Tab("Products")]
    [PriceListItemsEditor, SkipNameCheck]
    public List<PriceListItemsRow> ItemList { get; set; }
}