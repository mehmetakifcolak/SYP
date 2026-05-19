using Serenity.ComponentModel;

namespace SYP.Catalog.Forms;

[FormScript("Catalog.ProductPackingEditor")]
[BasedOnRow(typeof(ProductPackingRow), CheckNames = true)]
public class ProductPackingEditorForm
{
    public string Code { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public bool IsActive { get; set; }
}
