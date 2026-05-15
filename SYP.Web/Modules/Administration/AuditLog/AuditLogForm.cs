namespace SYP.Administration.Forms;

[FormScript("Administration.AuditLog")]
[BasedOnRow(typeof(AuditLogRow), CheckNames = true)]
public class AuditLogForm
{
    [HalfWidth, ReadOnly(true)]
    public DateTime ActionDate { get; set; }

    [HalfWidth, ReadOnly(true)]
    public string ActionType { get; set; }

    [HalfWidth, ReadOnly(true)]
    public string EntityType { get; set; }

    [HalfWidth, ReadOnly(true)]
    public string EntityId { get; set; }

    [ReadOnly(true)]
    public string EntityName { get; set; }

    [HalfWidth, ReadOnly(true)]
    public string Username { get; set; }

    [HalfWidth, ReadOnly(true)]
    public string IpAddress { get; set; }

    [ReadOnly(true)]
    public string ChangedFields { get; set; }

    [TextAreaEditor(Rows = 10), ReadOnly(true)]
    public string OldValues { get; set; }

    [TextAreaEditor(Rows = 10), ReadOnly(true)]
    public string NewValues { get; set; }
}
