using Serenity.ComponentModel;

namespace SYP.Catalog.Forms;

[FormScript("Catalog.Brands")]
[BasedOnRow(typeof(BrandsRow), CheckNames = true)]
public class BrandsForm
{
    public string Name { get; set; }

    [TextAreaEditor(Rows = 3)]
    public string Description { get; set; }

    public string Logo { get; set; }

    [DefaultValue(1)]
    public short IsActive { get; set; }
}