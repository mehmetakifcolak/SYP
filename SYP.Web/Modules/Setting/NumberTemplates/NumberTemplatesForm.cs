using Serenity.ComponentModel;

namespace SYP.Setting.Forms;

[FormScript("Setting.NumberTemplates")]
[BasedOnRow(typeof(NumberTemplatesRow), CheckNames = true)]
public class NumberTemplatesForm
{
    [Category("Template Settings")]
    [DisplayName("Template Type"), EnumEditor]
    public global::_Ext.NumberTemplateType Type { get; set; }

    [Placeholder("Prefix (e.g., MUS-, URN-)")]
    public string Prefix { get; set; }

    [Placeholder("Leave blank to exclude date"), Hint(@"
Example Formats:
yyyy     => 2024
yy       => 24
yyyyMM   => 202401
yyyyMMdd => 20240115
MMyyyy   => 012024
")]
    public string DateFormat { get; set; }

    [Placeholder("Suffix (after date)")]
    public string Suffix { get; set; }

    [Placeholder("Serial number length (e.g., 5 => 00001)")]
    public int Length { get; set; }

    [DefaultValue(true)]
    public bool Active { get; set; }
}