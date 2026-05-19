using Serenity.ComponentModel;

namespace SYP.Catalog.Forms;

[FormScript("Catalog.ProductPacking")]
[BasedOnRow(typeof(ProductPackingRow), CheckNames = true)]
public class ProductPackingForm
{
    public string Code { get; set; }
    public string Name { get; set; }

    [TextAreaEditor(Rows = 3)]
    public string Description { get; set; }

    public int Quantity { get; set; }

    [DefaultValue(true)]
    public bool IsActive { get; set; }
}
