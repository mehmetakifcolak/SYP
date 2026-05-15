namespace SYP.Email.Columns;

[ColumnsScript("Email.EmailLogs")]
[BasedOnRow(typeof(EmailLogsRow), CheckNames = true)]
public class EmailLogsColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, Width(80)]
    public long Id { get; set; }

    [Width(250)]
    public string EmailSubject { get; set; }

    [Width(200)]
    public string ToAddress { get; set; }

    [Width(100)]
    public int Status { get; set; }

    [Width(300)]
    public string StatusMessage { get; set; }

    [Width(80)]
    public int Duration { get; set; }

    [Width(150)]
    public DateTime InsertDate { get; set; }
}
