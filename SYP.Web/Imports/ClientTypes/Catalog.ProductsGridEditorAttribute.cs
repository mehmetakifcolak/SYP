namespace SYP.Catalog;

public partial class ProductsGridEditorAttribute : CustomEditorAttribute
{
    public const string Key = "SYP.Catalog.ProductsGridEditor";

    public ProductsGridEditorAttribute()
        : base(Key)
    {
    }
}