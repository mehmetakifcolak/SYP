namespace SYP.Warehouse;

public partial class StockEntryDetailsEditorAttribute : CustomEditorAttribute
{
    public const string Key = "SYP.Warehouse.StockEntryDetailsEditor";

    public StockEntryDetailsEditorAttribute()
        : base(Key)
    {
    }
}