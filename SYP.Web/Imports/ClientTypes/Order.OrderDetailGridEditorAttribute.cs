namespace SYP.Order;

public partial class OrderDetailGridEditorAttribute : CustomEditorAttribute
{
    public const string Key = "SYP.Order.OrderDetailGridEditor";

    public OrderDetailGridEditorAttribute()
        : base(Key)
    {
    }
}