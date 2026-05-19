namespace SYP.Catalog.Columns;

[ColumnsScript("Catalog.ProductPacking")]
[BasedOnRow(typeof(ProductPackingRow), CheckNames = true)]
public class ProductPackingColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
    public int Id { get; set; }
    [EditLink]
    public string Code { get; set; }
    [EditLink]
    public string Name { get; set; }
    public int Quantity { get; set; }
    public bool IsActive { get; set; }
    public DateTime InsertDate { get; set; }
}
