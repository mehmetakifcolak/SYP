namespace SYP.Catalog.Forms;

[FormScript("Catalog.PriceListsEditor")]
[BasedOnRow(typeof(PriceListsRow), CheckNames = true)]
public class PriceListsEditorForm
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? CurrencyId { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public short IsActive { get; set; }
    public bool IsDefault { get; set; }
    public DateTime InsertDate { get; set; }
    public int InsertUserId { get; set; }
    public DateTime UpdateDate { get; set; }
    public int UpdateUserId { get; set; }
}