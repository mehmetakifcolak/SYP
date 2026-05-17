
namespace SYP.Email;

[ConnectionKey("Default"), Module("Email"), TableName("EmailQueue")]
[DisplayName("Email Queue"), InstanceName("Email")]
[NavigationPermission("Email:EmailQueue:Navigation")]
[ReadPermission("Email:EmailQueue:Read")]
[InsertPermission("Email:EmailQueue:Insert")]
[UpdatePermission("Email:EmailQueue:Update")]
[DeletePermission("Email:EmailQueue:Delete")]
public sealed class EmailQueueRow : Row<EmailQueueRow.RowFields>, IIdRow, INameRow
{
    const string jSmtpSettings = nameof(jSmtpSettings);
    const string jTemplate = nameof(jTemplate);

    [DisplayName("Id"), Identity, IdProperty]
    public long? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int64Field Id; }

    [DisplayName("SMTP Settings"), ForeignKey(typeof(SmtpSettingsRow)), LeftJoin(jSmtpSettings)]
    [LookupEditor(typeof(SmtpSettingsRow), FilterField = "IsActive", FilterValue = true)]
    public int? SmtpSettingsId { get => fields.SmtpSettingsId[this]; set => fields.SmtpSettingsId[this] = value; }
    public partial class RowFields { public Int32Field SmtpSettingsId; }

    [DisplayName("Template"), ForeignKey(typeof(EmailTemplatesRow)), LeftJoin(jTemplate)]
    [LookupEditor(typeof(EmailTemplatesRow), FilterField = "IsActive", FilterValue = true)]
    public int? TemplateId { get => fields.TemplateId[this]; set => fields.TemplateId[this] = value; }
    public partial class RowFields { public Int32Field TemplateId; }

    [DisplayName("To Addresses"), NotNull]
    public string ToAddresses { get => fields.ToAddresses[this]; set => fields.ToAddresses[this] = value; }
    public partial class RowFields { public StringField ToAddresses; }

    [DisplayName("CC")]
    public string CcAddresses { get => fields.CcAddresses[this]; set => fields.CcAddresses[this] = value; }
    public partial class RowFields { public StringField CcAddresses; }

    [DisplayName("BCC")]
    public string BccAddresses { get => fields.BccAddresses[this]; set => fields.BccAddresses[this] = value; }
    public partial class RowFields { public StringField BccAddresses; }

    [DisplayName("From Address"), Size(200)]
    public string FromAddress { get => fields.FromAddress[this]; set => fields.FromAddress[this] = value; }
    public partial class RowFields { public StringField FromAddress; }

    [DisplayName("From Name"), Size(200)]
    public string FromName { get => fields.FromName[this]; set => fields.FromName[this] = value; }
    public partial class RowFields { public StringField FromName; }

    [DisplayName("Reply To Address"), Size(200)]
    public string ReplyToAddress { get => fields.ReplyToAddress[this]; set => fields.ReplyToAddress[this] = value; }
    public partial class RowFields { public StringField ReplyToAddress; }

    [DisplayName("Subject"), Size(500), NotNull, QuickSearch, NameProperty]
    public string Subject { get => fields.Subject[this]; set => fields.Subject[this] = value; }
    public partial class RowFields { public StringField Subject; }

    [DisplayName("Body (HTML)"), NotNull]
    public string Body { get => fields.Body[this]; set => fields.Body[this] = value; }
    public partial class RowFields { public StringField Body; }

    [DisplayName("Body (Text)")]
    public string BodyText { get => fields.BodyText[this]; set => fields.BodyText[this] = value; }
    public partial class RowFields { public StringField BodyText; }

    [DisplayName("Template Data")]
    public string TemplateData { get => fields.TemplateData[this]; set => fields.TemplateData[this] = value; }
    public partial class RowFields { public StringField TemplateData; }

    [DisplayName("Priority"), NotNull, DefaultValue(2)]
    public EmailPriority? Priority { get => (EmailPriority?)fields.Priority[this]; set => fields.Priority[this] = (int?)value; }
    public partial class RowFields { public Int32Field Priority; }

    [DisplayName("Status"), NotNull, DefaultValue(0)]
    public EmailQueueStatus? Status { get => (EmailQueueStatus?)fields.Status[this]; set => fields.Status[this] = (int?)value; }
    public partial class RowFields { public Int32Field Status; }

    [DisplayName("Scheduled At")]
    public DateTime? ScheduledAt { get => fields.ScheduledAt[this]; set => fields.ScheduledAt[this] = value; }
    public partial class RowFields { public DateTimeField ScheduledAt; }

    [DisplayName("Processed At")]
    public DateTime? ProcessedAt { get => fields.ProcessedAt[this]; set => fields.ProcessedAt[this] = value; }
    public partial class RowFields { public DateTimeField ProcessedAt; }

    [DisplayName("Sent At")]
    public DateTime? SentAt { get => fields.SentAt[this]; set => fields.SentAt[this] = value; }
    public partial class RowFields { public DateTimeField SentAt; }

    [DisplayName("Error Message")]
    public string ErrorMessage { get => fields.ErrorMessage[this]; set => fields.ErrorMessage[this] = value; }
    public partial class RowFields { public StringField ErrorMessage; }

    [DisplayName("Retry Count"), NotNull, DefaultValue(0)]
    public int? RetryCount { get => fields.RetryCount[this]; set => fields.RetryCount[this] = value; }
    public partial class RowFields { public Int32Field RetryCount; }

    [DisplayName("Next Retry At")]
    public DateTime? NextRetryAt { get => fields.NextRetryAt[this]; set => fields.NextRetryAt[this] = value; }
    public partial class RowFields { public DateTimeField NextRetryAt; }

    [DisplayName("Reference Type"), Size(100)]
    public string ReferenceType { get => fields.ReferenceType[this]; set => fields.ReferenceType[this] = value; }
    public partial class RowFields { public StringField ReferenceType; }

    [DisplayName("Reference ID"), Size(100)]
    public string ReferenceId { get => fields.ReferenceId[this]; set => fields.ReferenceId[this] = value; }
    public partial class RowFields { public StringField ReferenceId; }

    [DisplayName("Insert Date"), Insertable(false), Updatable(false)]
    public DateTime? InsertDate { get => fields.InsertDate[this]; set => fields.InsertDate[this] = value; }
    public partial class RowFields { public DateTimeField InsertDate; }

    [DisplayName("Insert User"), Insertable(false), Updatable(false)]
    public int? InsertUserId { get => fields.InsertUserId[this]; set => fields.InsertUserId[this] = value; }
    public partial class RowFields { public Int32Field InsertUserId; }

    #region Foreign Fields

    [DisplayName("SMTP Profile Name"), Expression($"{jSmtpSettings}.[Name]")]
    public string SmtpSettingsName { get => fields.SmtpSettingsName[this]; set => fields.SmtpSettingsName[this] = value; }
    public partial class RowFields { public StringField SmtpSettingsName; }

    [DisplayName("Template Name"), Expression($"{jTemplate}.[Name]")]
    public string TemplateName { get => fields.TemplateName[this]; set => fields.TemplateName[this] = value; }
    public partial class RowFields { public StringField TemplateName; }

    #endregion

    public partial class RowFields : RowFieldsBase { }
}
