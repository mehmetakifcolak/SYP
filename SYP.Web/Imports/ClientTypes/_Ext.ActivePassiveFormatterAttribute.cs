namespace _Ext;

public partial class ActivePassiveFormatterAttribute : CustomFormatterAttribute
{
    public const string Key = "_Ext.ActivePassiveFormatter";

    public ActivePassiveFormatterAttribute()
        : base(Key)
    {
    }
}