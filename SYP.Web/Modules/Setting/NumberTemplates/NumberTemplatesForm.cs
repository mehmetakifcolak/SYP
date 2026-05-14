using Serenity.ComponentModel;

namespace SYP.Setting.Forms;

[FormScript("Setting.NumberTemplates")]
[BasedOnRow(typeof(NumberTemplatesRow), CheckNames = true)]
public class NumberTemplatesForm
{
    [Category("Şablon Ayarları")]
    [DisplayName("Şablon Tipi"), EnumEditor]
    public global::_Ext.NumberTemplateType Type { get; set; }

    [Placeholder("Ön Ek (örn: MUS-, URN-)")]
    public string Prefix { get; set; }

    [Placeholder("Boş bırakılırsa tarih eklenmez"), Hint(@"
Örnek Formatlar:
yyyy     => 2024
yy       => 24
yyyyMM   => 202401
yyyyMMdd => 20240115
MMyyyy   => 012024
")]
    public string DateFormat { get; set; }

    [Placeholder("Son Ek (tarihten sonra)")]
    public string Suffix { get; set; }

    [Placeholder("Seri numarası uzunluğu (örn: 5 => 00001)")]
    public int Length { get; set; }

    [DefaultValue(true)]
    public bool Active { get; set; }
}