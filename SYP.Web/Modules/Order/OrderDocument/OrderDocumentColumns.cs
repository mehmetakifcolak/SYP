namespace SYP.Order.Columns;

[ColumnsScript("Order.OrderDocument")]
[BasedOnRow(typeof(OrderDocumentRow), CheckNames = true)]
public class OrderDocumentColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
    public int Id { get; set; }
    public string OrderNumber { get; set; }
    public int DocumentType { get; set; }
    [EditLink]
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public long FileSize { get; set; }
    public string MimeType { get; set; }
    public string UploadedByUserUsername { get; set; }
    public DateTime UploadDate { get; set; }
    public bool IsActive { get; set; }
    public string Notes { get; set; }
}