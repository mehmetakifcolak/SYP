namespace SYP.Products;

public partial class ProductCategoryGridEditorAttribute : CustomEditorAttribute
{
    public const string Key = "SYP.Products.ProductCategoryGridEditor";

    public ProductCategoryGridEditorAttribute()
        : base(Key)
    {
    }
}