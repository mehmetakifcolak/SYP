namespace SYP.Email.Columns;

[ColumnsScript("Email.EmailQueue")]
[BasedOnRow(typeof(EmailQueueRow), CheckNames = true)]
public class EmailQueueColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, Width(80)]
    public long Id { get; set; }

    [Width(100)]
    public string SmtpSettingsName { get; set; }

    [EditLink, Width(250)]
    public string Subject { get; set; }

    [Width(200)]
    public string ToAddresses { get; set; }

    [Width(100)]
    public int Priority { get; set; }

    [Width(120)]
    public int Status { get; set; }

    [Width(80)]
    public int RetryCount { get; set; }

    [Width(150)]
    public DateTime ScheduledAt { get; set; }

    [Width(150)]
    public DateTime SentAt { get; set; }

    [Width(150)]
    public DateTime InsertDate { get; set; }
}
