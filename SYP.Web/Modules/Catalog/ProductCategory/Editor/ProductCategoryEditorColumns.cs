namespace SYP.Catalog.Editor;

[ColumnsScript("Catalog.ProductCategoryEditor")]
[BasedOnRow(typeof(ProductCategoryRow), CheckNames = true)]
public class ProductCategoryEditorColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
    public int Id { get; set; }
    public string ParentName { get; set; }
    [EditLink]
    public string Name { get; set; }
    public string FullPath { get; set; }
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}