namespace SYP.Catalog.Forms;

[FormScript("Catalog.Brands")]
[BasedOnRow(typeof(BrandsRow), CheckNames = true)]
public class BrandsForm
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Logo { get; set; }
    public bool IsActive { get; set; }
}
