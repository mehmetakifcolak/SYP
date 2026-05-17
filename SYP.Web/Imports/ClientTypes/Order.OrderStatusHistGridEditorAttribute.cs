namespace SYP.Order;

public partial class OrderStatusHistGridEditorAttribute : CustomEditorAttribute
{
    public const string Key = "SYP.Order.OrderStatusHistGridEditor";

    public OrderStatusHistGridEditorAttribute()
        : base(Key)
    {
    }
}