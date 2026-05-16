namespace SYP.Catalog;

public partial class PriceListItemsEditorAttribute : CustomEditorAttribute
{
    public const string Key = "SYP.Catalog.PriceListItemsEditor";

    public PriceListItemsEditorAttribute()
        : base(Key)
    {
    }
}