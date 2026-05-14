namespace SYP.Setting.Forms;

[FormScript("Setting.NumberTemplatesEditor")]
[BasedOnRow(typeof(NumberTemplatesRow), CheckNames = true)]
public class NumberTemplatesEditorForm
{
    public string Prefix { get; set; }
    public string DateFormat { get; set; }
    public string Suffix { get; set; }
    public int Length { get; set; }
    public int LengthText { get; set; }
    public bool Active { get; set; }
    public int Type { get; set; }
    public int DepartmentId { get; set; }
    public int InsertUserId { get; set; }
    public DateTime InsertDate { get; set; }
    public int UpdateUserId { get; set; }
    public DateTime UpdateDate { get; set; }
    public int TenantId { get; set; }
}