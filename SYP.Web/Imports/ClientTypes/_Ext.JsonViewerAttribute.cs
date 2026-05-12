namespace _Ext;

public partial class JsonViewerAttribute : CustomEditorAttribute
{
    public const string Key = "_Ext.JsonViewer";

    public JsonViewerAttribute()
        : base(Key)
    {
    }
}