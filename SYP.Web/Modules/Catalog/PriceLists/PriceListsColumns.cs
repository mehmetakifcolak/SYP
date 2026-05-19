namespace SYP.Catalog.Columns;

[ColumnsScript("Catalog.PriceLists")]
[BasedOnRow(typeof(PriceListsRow), CheckNames = true)]
public class PriceListsColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, Width(80)]
    public int Id { get; set; }

    [EditLink, Width(120)]
    public string Code { get; set; }

    [Width(200)]
    public string Name { get; set; }

    [Width(150)]
    public PriceListType Type { get; set; }

    [Width(250)]
    public string Description { get; set; }

    [Width(100)]
    public string CurrencyCode { get; set; }

    [Width(120), DisplayFormat("dd.MM.yyyy")]
    public DateTime ValidFrom { get; set; }

    [Width(120), DisplayFormat("dd.MM.yyyy")]
    public DateTime ValidTo { get; set; }

    [Width(80)]
    public short IsActive { get; set; }

    [Width(100)]
    public bool IsDefault { get; set; }
}