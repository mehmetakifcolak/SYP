namespace SYP.Order.Forms;

[FormScript("Order.OrderDocumentEditor")]
[BasedOnRow(typeof(OrderDocumentRow), CheckNames = true)]
public class OrderDocumentEditorForm
{
    public int OrderId { get; set; }
    public int DocumentType { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public long FileSize { get; set; }
    public string MimeType { get; set; }
    public int UploadedByUserId { get; set; }
    public DateTime UploadDate { get; set; }
    public bool IsActive { get; set; }
    public string Notes { get; set; }
}