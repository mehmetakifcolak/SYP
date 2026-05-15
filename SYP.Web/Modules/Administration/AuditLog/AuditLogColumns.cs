namespace SYP.Administration.Columns;

[ColumnsScript("Administration.AuditLog")]
[BasedOnRow(typeof(AuditLogRow), CheckNames = true)]
public class AuditLogColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, Width(80)]
    public long Id { get; set; }

    [Width(150)]
    public DateTime ActionDate { get; set; }

    [Width(100)]
    public string ActionType { get; set; }

    [Width(150)]
    public string EntityType { get; set; }

    [EditLink, Width(200)]
    public string EntityName { get; set; }

    [Width(100)]
    public string EntityId { get; set; }

    [Width(120)]
    public string Username { get; set; }

    [Width(120)]
    public string IpAddress { get; set; }

    [Width(200)]
    public string ChangedFields { get; set; }
}
