namespace SYP.Catalog.Forms;

[FormScript("Catalog.ProductsEditor")]
[BasedOnRow(typeof(ProductsRow), CheckNames = true)]
public class ProductsEditorForm
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Name2 { get; set; }
    public string Description { get; set; }
    public string Barcode { get; set; }
    public string Unit { get; set; }
    public string Currency { get; set; }
    public string VatRate { get; set; }
    public DateTime InsertDate { get; set; }
    public int InsertUserId { get; set; }
    public DateTime UpdateDate { get; set; }
    public int UpdateUserId { get; set; }
    public short IsActive { get; set; }
}