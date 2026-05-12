namespace _Ext;

public partial class MonthYearFormatterAttribute : CustomFormatterAttribute
{
    public const string Key = "_Ext.MonthYearFormatter";

    public MonthYearFormatterAttribute()
        : base(Key)
    {
    }
}