using Serenity.ComponentModel;
using System.ComponentModel;

namespace SYP.Setting.Columns;

[ColumnsScript("Setting.NumberTemplates")]
[BasedOnRow(typeof(NumberTemplatesRow), CheckNames = true)]
public class NumberTemplatesColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, Width(80)]
    public int Id { get; set; }

    [DisplayName("Şablon Tipi"), Width(150)]
    public global::_Ext.NumberTemplateType Type { get; set; }

    [EditLink, Width(100)]
    public string Prefix { get; set; }

    [Width(120)]
    public string DateFormat { get; set; }

    [Width(80)]
    public string Suffix { get; set; }

    [DisplayName("Uzunluk"), Width(80)]
    public int Length { get; set; }

    [DisplayName("Aktif"), Width(80)]
    public bool Active { get; set; }
}