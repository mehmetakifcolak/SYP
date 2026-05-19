
namespace SYP.Email.Services;

public interface IEmailQueueSender
{
    Task<long> QueueEmailAsync(QueueEmailRequest request);
    Task<long> QueueTemplateEmailAsync(QueueTemplateEmailRequest request);
    Task<long> QueueScheduledEmailAsync(QueueEmailRequest request, DateTime scheduledAt);
    Task<bool> CancelEmailAsync(long emailQueueId);
    Task<bool> ResendEmailAsync(long emailQueueId);
}

public class QueueEmailRequest
{
    public List<string> To { get; set; } = new();
    public List<string> Cc { get; set; }
    public List<string> Bcc { get; set; }
    public string Subject { get; set; } = "";
    public string Body { get; set; } = "";
    public string BodyText { get; set; }
    public string FromAddress { get; set; }
    public string FromName { get; set; }
    public string ReplyTo { get; set; }
    public EmailPriority Priority { get; set; } = EmailPriority.Normal;
    public int? SmtpSettingsId { get; set; }
    public string ReferenceType { get; set; }
    public string ReferenceId { get; set; }
    public List<EmailAttachmentRequest> Attachments { get; set; }
}

public class QueueTemplateEmailRequest
{
    public string TemplateKey { get; set; } = "";
    public List<string> To { get; set; } = new();
    public List<string> Cc { get; set; }
    public List<string> Bcc { get; set; }
    public Dictionary<string, object> TemplateData { get; set; }
    public string LanguageId { get; set; }
    public EmailPriority Priority { get; set; } = EmailPriority.Normal;
    public int? SmtpSettingsId { get; set; }
    public string ReferenceType { get; set; }
    public string ReferenceId { get; set; }
    public List<EmailAttachmentRequest> Attachments { get; set; }
}

public class EmailAttachmentRequest
{
    public string FileName { get; set; } = "";
    public string ContentType { get; set; } = "application/octet-stream";
    public byte[] Content { get; set; }
    public string FilePath { get; set; }
    public bool IsInline { get; set; }
    public string ContentId { get; set; }
}
