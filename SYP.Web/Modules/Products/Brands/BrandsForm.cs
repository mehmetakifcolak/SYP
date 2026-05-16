namespace SYP.Products.Forms;

[FormScript("Products.Brands")]
[BasedOnRow(typeof(BrandsRow), CheckNames = true)]
public class BrandsForm
{
    public string Name { get; set; }

    [TextAreaEditor(Rows = 3)]
    public string Description { get; set; }

    public string Logo { get; set; }

    public bool IsActive { get; set; }
}