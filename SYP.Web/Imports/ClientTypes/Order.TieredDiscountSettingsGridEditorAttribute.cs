namespace SYP.Order;

public partial class TieredDiscountSettingsGridEditorAttribute : CustomEditorAttribute
{
    public const string Key = "SYP.Order.TieredDiscountSettingsGridEditor";

    public TieredDiscountSettingsGridEditorAttribute()
        : base(Key)
    {
    }
}