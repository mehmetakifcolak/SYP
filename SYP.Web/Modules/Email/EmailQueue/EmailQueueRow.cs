using _Ext;

namespace SYP.Email;

[ConnectionKey("Default"), Module("Email"), TableName("EmailQueue")]
[DisplayName("Email Kuyruğu"), InstanceName("Email")]
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

    [DisplayName("SMTP Ayarı"), ForeignKey(typeof(SmtpSettingsRow)), LeftJoin(jSmtpSettings)]
    [LookupEditor(typeof(SmtpSettingsRow), FilterField = "IsActive", FilterValue = true)]
    public int? SmtpSettingsId { get => fields.SmtpSettingsId[this]; set => fields.SmtpSettingsId[this] = value; }
    public partial class RowFields { public Int32Field SmtpSettingsId; }

    [DisplayName("Şablon"), ForeignKey(typeof(EmailTemplatesRow)), LeftJoin(jTemplate)]
    [LookupEditor(typeof(EmailTemplatesRow), FilterField = "IsActive", FilterValue = true)]
    public int? TemplateId { get => fields.TemplateId[this]; set => fields.TemplateId[this] = value; }
    public partial class RowFields { public Int32Field TemplateId; }

    [DisplayName("Alıcılar"), NotNull]
    public string ToAddresses { get => fields.ToAddresses[this]; set => fields.ToAddresses[this] = value; }
    public partial class RowFields { public StringField ToAddresses; }

    [DisplayName("CC")]
    public string CcAddresses { get => fields.CcAddresses[this]; set => fields.CcAddresses[this] = value; }
    public partial class RowFields { public StringField CcAddresses; }

    [DisplayName("BCC")]
    public string BccAddresses { get => fields.BccAddresses[this]; set => fields.BccAddresses[this] = value; }
    public partial class RowFields { public StringField BccAddresses; }

    [DisplayName("Gönderici Email"), Size(200)]
    public string FromAddress { get => fields.FromAddress[this]; set => fields.FromAddress[this] = value; }
    public partial class RowFields { public StringField FromAddress; }

    [DisplayName("Gönderici Adı"), Size(200)]
    public string FromName { get => fields.FromName[this]; set => fields.FromName[this] = value; }
    public partial class RowFields { public StringField FromName; }

    [DisplayName("Yanıt Adresi"), Size(200)]
    public string ReplyToAddress { get => fields.ReplyToAddress[this]; set => fields.ReplyToAddress[this] = value; }
    public partial class RowFields { public StringField ReplyToAddress; }

    [DisplayName("Konu"), Size(500), NotNull, QuickSearch, NameProperty]
    public string Subject { get => fields.Subject[this]; set => fields.Subject[this] = value; }
    public partial class RowFields { public StringField Subject; }

    [DisplayName("İçerik (HTML)"), NotNull]
    public string Body { get => fields.Body[this]; set => fields.Body[this] = value; }
    public partial class RowFields { public StringField Body; }

    [DisplayName("İçerik (Text)")]
    public string BodyText { get => fields.BodyText[this]; set => fields.BodyText[this] = value; }
    public partial class RowFields { public StringField BodyText; }

    [DisplayName("Şablon Verileri")]
    public string TemplateData { get => fields.TemplateData[this]; set => fields.TemplateData[this] = value; }
    public partial class RowFields { public StringField TemplateData; }

    [DisplayName("Öncelik"), NotNull, DefaultValue(2)]
    public EmailPriority? Priority { get => (EmailPriority?)fields.Priority[this]; set => fields.Priority[this] = (int?)value; }
    public partial class RowFields { public Int32Field Priority; }

    [DisplayName("Durum"), NotNull, DefaultValue(0)]
    public EmailQueueStatus? Status { get => (EmailQueueStatus?)fields.Status[this]; set => fields.Status[this] = (int?)value; }
    public partial class RowFields { public Int32Field Status; }

    [DisplayName("Zamanlanmış Gönderim")]
    public DateTime? ScheduledAt { get => fields.ScheduledAt[this]; set => fields.ScheduledAt[this] = value; }
    public partial class RowFields { public DateTimeField ScheduledAt; }

    [DisplayName("İşleme Alınma")]
    public DateTime? ProcessedAt { get => fields.ProcessedAt[this]; set => fields.ProcessedAt[this] = value; }
    public partial class RowFields { public DateTimeField ProcessedAt; }

    [DisplayName("Gönderim Tarihi")]
    public DateTime? SentAt { get => fields.SentAt[this]; set => fields.SentAt[this] = value; }
    public partial class RowFields { public DateTimeField SentAt; }

    [DisplayName("Hata Mesajı")]
    public string ErrorMessage { get => fields.ErrorMessage[this]; set => fields.ErrorMessage[this] = value; }
    public partial class RowFields { public StringField ErrorMessage; }

    [DisplayName("Deneme Sayısı"), NotNull, DefaultValue(0)]
    public int? RetryCount { get => fields.RetryCount[this]; set => fields.RetryCount[this] = value; }
    public partial class RowFields { public Int32Field RetryCount; }

    [DisplayName("Sonraki Deneme")]
    public DateTime? NextRetryAt { get => fields.NextRetryAt[this]; set => fields.NextRetryAt[this] = value; }
    public partial class RowFields { public DateTimeField NextRetryAt; }

    [DisplayName("Referans Tipi"), Size(100)]
    public string ReferenceType { get => fields.ReferenceType[this]; set => fields.ReferenceType[this] = value; }
    public partial class RowFields { public StringField ReferenceType; }

    [DisplayName("Referans ID"), Size(100)]
    public string ReferenceId { get => fields.ReferenceId[this]; set => fields.ReferenceId[this] = value; }
    public partial class RowFields { public StringField ReferenceId; }

    [DisplayName("Oluşturma Tarihi"), Insertable(false), Updatable(false)]
    public DateTime? InsertDate { get => fields.InsertDate[this]; set => fields.InsertDate[this] = value; }
    public partial class RowFields { public DateTimeField InsertDate; }

    [DisplayName("Oluşturan"), Insertable(false), Updatable(false)]
    public int? InsertUserId { get => fields.InsertUserId[this]; set => fields.InsertUserId[this] = value; }
    public partial class RowFields { public Int32Field InsertUserId; }

    #region Foreign Fields

    [DisplayName("SMTP Profil Adı"), Expression($"{jSmtpSettings}.[Name]")]
    public string SmtpSettingsName { get => fields.SmtpSettingsName[this]; set => fields.SmtpSettingsName[this] = value; }
    public partial class RowFields { public StringField SmtpSettingsName; }

    [DisplayName("Şablon Adı"), Expression($"{jTemplate}.[Name]")]
    public string TemplateName { get => fields.TemplateName[this]; set => fields.TemplateName[this] = value; }
    public partial class RowFields { public StringField TemplateName; }

    #endregion

    public partial class RowFields : RowFieldsBase { }
}
