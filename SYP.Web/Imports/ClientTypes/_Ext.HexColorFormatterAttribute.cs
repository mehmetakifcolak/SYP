namespace _Ext;

public partial class HexColorFormatterAttribute : CustomFormatterAttribute
{
    public const string Key = "_Ext.HexColorFormatter";

    public HexColorFormatterAttribute()
        : base(Key)
    {
    }
}