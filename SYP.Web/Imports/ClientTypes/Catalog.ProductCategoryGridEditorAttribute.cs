namespace SYP.Catalog;

public partial class ProductCategoryGridEditorAttribute : CustomEditorAttribute
{
    public const string Key = "SYP.Catalog.ProductCategoryGridEditor";

    public ProductCategoryGridEditorAttribute()
        : base(Key)
    {
    }
}