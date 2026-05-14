using Serenity.ComponentModel;

namespace SYP.Catalog.Forms;

[FormScript("Catalog.Products")]
[BasedOnRow(typeof(ProductsRow), CheckNames = true)]
public class ProductsForm
{
    [Placeholder("Boş bırakılırsa otomatik oluşturulur")]
    public string Code { get; set; }

    public string Name { get; set; }
    public string Name2 { get; set; }
    public string Description { get; set; }
    public string Barcode { get; set; }

    [Category("Görsel")]
    public string ProductImage { get; set; }
}