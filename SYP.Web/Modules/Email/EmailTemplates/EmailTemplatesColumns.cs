namespace SYP.Email.Columns;

[ColumnsScript("Email.EmailTemplates")]
[BasedOnRow(typeof(EmailTemplatesRow), CheckNames = true)]
public class EmailTemplatesColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, Width(80)]
    public int Id { get; set; }

    [EditLink, Width(150)]
    public string TemplateKey { get; set; }

    [Width(200)]
    public string Name { get; set; }

    [Width(250)]
    public string Subject { get; set; }

    [Width(100)]
    public string Category { get; set; }

    [Width(80)]
    public string LanguageId { get; set; }

    [Width(80)]
    public bool IsActive { get; set; }
}
