namespace SYP.Email.Forms;

[FormScript("Email.EmailLogs")]
[BasedOnRow(typeof(EmailLogsRow), CheckNames = true)]
public class EmailLogsForm
{
    [ReadOnly(true)]
    public long EmailQueueId { get; set; }

    [ReadOnly(true)]
    public string EmailSubject { get; set; }

    [ReadOnly(true)]
    public string ToAddress { get; set; }

    [ReadOnly(true)]
    public int Status { get; set; }

    [ReadOnly(true), TextAreaEditor(Rows = 3)]
    public string StatusMessage { get; set; }

    [ReadOnly(true), TextAreaEditor(Rows = 3)]
    public string SmtpResponse { get; set; }

    [ReadOnly(true)]
    public DateTime ProcessStartTime { get; set; }

    [ReadOnly(true)]
    public DateTime ProcessEndTime { get; set; }

    [ReadOnly(true)]
    public int Duration { get; set; }

    [ReadOnly(true)]
    public DateTime InsertDate { get; set; }
}
