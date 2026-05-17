namespace SYP.Order;

public partial class OrderDocumentGridEditorAttribute : CustomEditorAttribute
{
    public const string Key = "SYP.Order.OrderDocumentGridEditor";

    public OrderDocumentGridEditorAttribute()
        : base(Key)
    {
    }
}