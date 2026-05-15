namespace SYP.Email.Forms;

[FormScript("Email.EmailTemplates")]
[BasedOnRow(typeof(EmailTemplatesRow), CheckNames = true)]
public class EmailTemplatesForm
{
    [Tab("Genel")]
    [HalfWidth]
    public string TemplateKey { get; set; }

    [HalfWidth]
    public string Name { get; set; }

    public string Subject { get; set; }

    [HalfWidth]
    public string Category { get; set; }

    [HalfWidth]
    public string LanguageId { get; set; }

    [TextAreaEditor(Rows = 3)]
    public string Description { get; set; }

    public bool IsActive { get; set; }

    [Tab("HTML İçerik")]
    [HtmlContentEditor(Rows = 20)]
    public string Body { get; set; }

    [Tab("Text İçerik")]
    [TextAreaEditor(Rows = 15)]
    public string BodyText { get; set; }
}
