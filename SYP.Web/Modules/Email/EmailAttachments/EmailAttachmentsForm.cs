namespace SYP.Email.Forms;

[FormScript("Email.EmailAttachments")]
[BasedOnRow(typeof(EmailAttachmentsRow), CheckNames = true)]
public class EmailAttachmentsForm
{
    [ReadOnly(true)]
    public long EmailQueueId { get; set; }

    [ReadOnly(true)]
    public string FileName { get; set; }

    [ReadOnly(true)]
    public string ContentType { get; set; }

    [ReadOnly(true)]
    public string FilePath { get; set; }

    [ReadOnly(true)]
    public long FileSize { get; set; }

    [ReadOnly(true)]
    public bool IsInline { get; set; }

    [ReadOnly(true)]
    public string ContentId { get; set; }

    [ReadOnly(true)]
    public DateTime InsertDate { get; set; }
}
