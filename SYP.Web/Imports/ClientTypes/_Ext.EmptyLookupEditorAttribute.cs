namespace _Ext;

public partial class EmptyLookupEditorAttribute : LookupEditorBaseAttribute
{
    public const string Key = "_Ext.EmptyLookupEditor";

    public EmptyLookupEditorAttribute()
        : base(Key)
    {
    }
}