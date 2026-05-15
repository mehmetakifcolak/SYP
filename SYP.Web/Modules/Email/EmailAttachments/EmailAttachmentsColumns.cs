namespace SYP.Email.Columns;

[ColumnsScript("Email.EmailAttachments")]
[BasedOnRow(typeof(EmailAttachmentsRow), CheckNames = true)]
public class EmailAttachmentsColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, Width(80)]
    public int Id { get; set; }

    [Width(100)]
    public long EmailQueueId { get; set; }

    [EditLink, Width(250)]
    public string FileName { get; set; }

    [Width(150)]
    public string ContentType { get; set; }

    [Width(100), AlignRight]
    public long FileSize { get; set; }

    [Width(80)]
    public bool IsInline { get; set; }

    [Width(150)]
    public DateTime InsertDate { get; set; }
}
