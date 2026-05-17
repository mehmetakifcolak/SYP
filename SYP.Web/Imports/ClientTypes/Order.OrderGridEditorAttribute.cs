namespace SYP.Order;

public partial class OrderGridEditorAttribute : CustomEditorAttribute
{
    public const string Key = "SYP.Order.OrderGridEditor";

    public OrderGridEditorAttribute()
        : base(Key)
    {
    }
}