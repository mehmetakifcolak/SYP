using _Ext;

namespace SYP.Email;

[ConnectionKey("Default"), Module("Email"), TableName("EmailLogs")]
[DisplayName("Email Logları"), InstanceName("Email Log")]
[NavigationPermission("Email:EmailLogs:Navigation")]
[ReadPermission("Email:EmailQueue:Read")]
public sealed class EmailLogsRow : Row<EmailLogsRow.RowFields>, IIdRow
{
    const string jEmailQueue = nameof(jEmailQueue);

    [DisplayName("Id"), Identity, IdProperty]
    public long? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int64Field Id; }

    [DisplayName("Email"), NotNull, ForeignKey(typeof(EmailQueueRow)), LeftJoin(jEmailQueue)]
    public long? EmailQueueId { get => fields.EmailQueueId[this]; set => fields.EmailQueueId[this] = value; }
    public partial class RowFields { public Int64Field EmailQueueId; }

    [DisplayName("Durum"), NotNull]
    public EmailLogStatus? Status { get => (EmailLogStatus?)fields.Status[this]; set => fields.Status[this] = (int?)value; }
    public partial class RowFields { public Int32Field Status; }

    [DisplayName("Durum Mesajı")]
    public string StatusMessage { get => fields.StatusMessage[this]; set => fields.StatusMessage[this] = value; }
    public partial class RowFields { public StringField StatusMessage; }

    [DisplayName("SMTP Yanıtı")]
    public string SmtpResponse { get => fields.SmtpResponse[this]; set => fields.SmtpResponse[this] = value; }
    public partial class RowFields { public StringField SmtpResponse; }

    [DisplayName("Alıcı Adresi"), Size(200)]
    public string ToAddress { get => fields.ToAddress[this]; set => fields.ToAddress[this] = value; }
    public partial class RowFields { public StringField ToAddress; }

    [DisplayName("İşlem Başlangıcı")]
    public DateTime? ProcessStartTime { get => fields.ProcessStartTime[this]; set => fields.ProcessStartTime[this] = value; }
    public partial class RowFields { public DateTimeField ProcessStartTime; }

    [DisplayName("İşlem Bitişi")]
    public DateTime? ProcessEndTime { get => fields.ProcessEndTime[this]; set => fields.ProcessEndTime[this] = value; }
    public partial class RowFields { public DateTimeField ProcessEndTime; }

    [DisplayName("Süre (ms)")]
    public int? Duration { get => fields.Duration[this]; set => fields.Duration[this] = value; }
    public partial class RowFields { public Int32Field Duration; }

    [DisplayName("Oluşturma Tarihi"), Insertable(false), Updatable(false)]
    public DateTime? InsertDate { get => fields.InsertDate[this]; set => fields.InsertDate[this] = value; }
    public partial class RowFields { public DateTimeField InsertDate; }

    #region Foreign Fields

    [DisplayName("Email Konu"), Expression($"{jEmailQueue}.[Subject]")]
    public string EmailSubject { get => fields.EmailSubject[this]; set => fields.EmailSubject[this] = value; }
    public partial class RowFields { public StringField EmailSubject; }

    #endregion

    public partial class RowFields : RowFieldsBase { }
}
