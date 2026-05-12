namespace _Ext;

public partial class YesNoFormatterAttribute : CustomFormatterAttribute
{
    public const string Key = "_Ext.YesNoFormatter";

    public YesNoFormatterAttribute()
        : base(Key)
    {
    }
}