namespace SYP.Email.Forms;

[FormScript("Email.EmailQueue")]
[BasedOnRow(typeof(EmailQueueRow), CheckNames = true)]
public class EmailQueueForm
{
    [Tab("Alıcılar")]
    [TextAreaEditor(Rows = 3), Placeholder("Her satıra bir email adresi")]
    public string ToAddresses { get; set; }

    [TextAreaEditor(Rows = 2)]
    public string CcAddresses { get; set; }

    [TextAreaEditor(Rows = 2)]
    public string BccAddresses { get; set; }

    [Tab("İçerik")]
    public int SmtpSettingsId { get; set; }
    public int TemplateId { get; set; }
    public string Subject { get; set; }

    [HtmlContentEditor(Rows = 15)]
    public string Body { get; set; }

    [TextAreaEditor(Rows = 5)]
    public string BodyText { get; set; }

    [Tab("Gönderici")]
    [HalfWidth, EmailAddressEditor]
    public string FromAddress { get; set; }

    [HalfWidth]
    public string FromName { get; set; }

    [EmailAddressEditor]
    public string ReplyToAddress { get; set; }

    [Tab("Zamanlanmış Gönderim")]
    [HalfWidth]
    public int Priority { get; set; }

    [HalfWidth]
    public DateTime ScheduledAt { get; set; }

    [Tab("Durum")]
    [ReadOnly(true)]
    public int Status { get; set; }

    [ReadOnly(true)]
    public int RetryCount { get; set; }

    [ReadOnly(true)]
    public DateTime ProcessedAt { get; set; }

    [ReadOnly(true)]
    public DateTime SentAt { get; set; }

    [ReadOnly(true)]
    public DateTime NextRetryAt { get; set; }

    [TextAreaEditor(Rows = 5), ReadOnly(true)]
    public string ErrorMessage { get; set; }

    [Tab("Referans")]
    [HalfWidth]
    public string ReferenceType { get; set; }

    [HalfWidth]
    public string ReferenceId { get; set; }

    [TextAreaEditor(Rows = 5)]
    public string TemplateData { get; set; }
}
