namespace SYP.Order.Endpoints;

public class UploadDekontRequest
{
    public int    OrderId     { get; set; }
    public string FileName    { get; set; }
    public string FileBase64  { get; set; }
    public string MimeType    { get; set; }
}
