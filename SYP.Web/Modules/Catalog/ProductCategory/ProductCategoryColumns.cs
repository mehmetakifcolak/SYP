namespace SYP.Catalog;

[ColumnsScript("Catalog.ProductCategory")]
[BasedOnRow(typeof(ProductCategoryRow), CheckNames = true)]
public class ProductCategoryColumns
{
    [EditLink, Width(300)]
    public string Name { get; set; }

    [Width(80)]
    public int SortOrder { get; set; }

    [Width(80)]
    public bool IsActive { get; set; }
}