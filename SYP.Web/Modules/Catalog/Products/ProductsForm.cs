using Serenity.ComponentModel;

namespace SYP.Catalog.Forms;

[FormScript("Catalog.Products")]
[BasedOnRow(typeof(ProductsRow), CheckNames = true)]
public class ProductsForm
{
    [Tab("General Information")]
    [HalfWidth, Placeholder("Will be auto-generated if left blank")]
    public string Code { get; set; }

    [HalfWidth]
    public string Barcode { get; set; }

    [HalfWidth]
    public int? CategoryId { get; set; }

    [HalfWidth]
    public int? BrandId { get; set; }

    [FullWidth]
    public string Name { get; set; }

    [FullWidth]
    public string Name2 { get; set; }

    [FullWidth, TextAreaEditor(Rows = 3)]
    public string Description { get; set; }

    [Tab("Pricing Information")]
    [HalfWidth]
    public int? UnitId { get; set; }

    [HalfWidth]
    public int? PackingId { get; set; }

    [HalfWidth]
    public int? CurrencyId { get; set; }

    [HalfWidth]
    public int? VatRateId { get; set; }

    [HalfWidth, ReadOnly(true)]
    public decimal? CurrentValidPrice { get; set; }

    [Tab("Image")]
    [FullWidth]
    public string ProductImage { get; set; }

    [Tab("Status")]
    public bool IsActive { get; set; }
}
