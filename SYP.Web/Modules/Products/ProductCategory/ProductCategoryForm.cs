namespace SYP.Products.Forms;

[FormScript("Products.ProductCategory")]
[BasedOnRow(typeof(ProductCategoryRow), CheckNames = true)]
public class ProductCategoryForm
{
    [LookupEditor("Products.ProductCategory")]
    public int ParentId { get; set; }

    public string Name { get; set; }

    [HalfWidth]
    public int SortOrder { get; set; }

    [HalfWidth]
    public bool IsActive { get; set; }
}