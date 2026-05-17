namespace SYP.Catalog.Editor;

[FormScript("Catalog.ProductCategoryEditor")]
[BasedOnRow(typeof(ProductCategoryRow), CheckNames = true)]
public class ProductCategoryEditorForm
{
    public int ParentId { get; set; }
    public string Name { get; set; }
    public string FullPath { get; set; }
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}