using Serenity.ComponentModel;
using System.ComponentModel;

namespace SYP.Catalog.Columns;

[ColumnsScript("Catalog.Products")]
[BasedOnRow(typeof(ProductsRow), CheckNames = true)]
public class ProductsColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, Width(80)]
    public int Id { get; set; }

    [DisplayName("Resim"), Width(70), global::_Ext.InlineImageFormatter(Thumb = true)]
    public string ProductImage { get; set; }

    [EditLink, Width(120)]
    public string Code { get; set; }

    [Width(200)]
    public string Name { get; set; }

    [Width(150)]
    public string CategoryName { get; set; }

    [Width(120)]
    public string BrandName { get; set; }

    [Width(80)]
    public string UnitName { get; set; }

    [Width(100), AlignRight, DisplayFormat("#,##0.0000")]
    public decimal? CurrentValidPrice { get; set; }

    [Width(80)]
    public string CurrencyCode { get; set; }

    [Width(80)]
    public string VatRateName { get; set; }

    [Width(120)]
    public string Barcode { get; set; }

    [Width(80)]
    public short IsActive { get; set; }
}
