namespace _Ext;

public partial class YesNoColoredFormatterAttribute : CustomFormatterAttribute
{
    public const string Key = "_Ext.YesNoColoredFormatter";

    public YesNoColoredFormatterAttribute()
        : base(Key)
    {
    }
}