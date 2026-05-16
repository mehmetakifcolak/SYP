namespace SYP.Email;

[ConnectionKey("Default"), Module("Email"), TableName("SmtpSettings")]
[DisplayName("SMTP Ayarları"), InstanceName("SMTP Ayarı")]
[LookupScript("Email.SmtpSettings", Permission = "Email:SmtpSettings:Read")]
[NavigationPermission("Email:SmtpSettings:Navigation")]
[ReadPermission("Email:SmtpSettings:Read")]
[InsertPermission("Email:SmtpSettings:Insert")]
[UpdatePermission("Email:SmtpSettings:Update")]
[DeletePermission("Email:SmtpSettings:Delete")]
[ServiceLookupPermission("Email:SmtpSettings:Read")]
public sealed class SmtpSettingsRow : Row<SmtpSettingsRow.RowFields>, IIdRow, INameRow, SYP.Administration.IAuditedRow
{
    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }

    [DisplayName("Profil Adı"), Size(100), NotNull, QuickSearch, NameProperty]
    public string Name { get => fields.Name[this]; set => fields.Name[this] = value; }
    public partial class RowFields { public StringField Name; }

    [DisplayName("SMTP Sunucu"), Size(200), NotNull]
    public string Host { get => fields.Host[this]; set => fields.Host[this] = value; }
    public partial class RowFields { public StringField Host; }

    [DisplayName("Port"), NotNull, DefaultValue(587)]
    public int? Port { get => fields.Port[this]; set => fields.Port[this] = value; }
    public partial class RowFields { public Int32Field Port; }

    [DisplayName("SSL Kullan"), NotNull, DefaultValue(true)]
    public bool? UseSsl { get => fields.UseSsl[this]; set => fields.UseSsl[this] = value; }
    public partial class RowFields { public BooleanField UseSsl; }

    [DisplayName("Kullanıcı Adı"), Size(200)]
    public string Username { get => fields.Username[this]; set => fields.Username[this] = value; }
    public partial class RowFields { public StringField Username; }

    [DisplayName("Şifre"), Size(500)]
    public string Password { get => fields.Password[this]; set => fields.Password[this] = value; }
    public partial class RowFields { public StringField Password; }

    [DisplayName("Gönderici Email"), Size(200), NotNull]
    public string FromAddress { get => fields.FromAddress[this]; set => fields.FromAddress[this] = value; }
    public partial class RowFields { public StringField FromAddress; }

    [DisplayName("Gönderici Adı"), Size(200)]
    public string FromName { get => fields.FromName[this]; set => fields.FromName[this] = value; }
    public partial class RowFields { public StringField FromName; }

    [DisplayName("Varsayılan"), NotNull, DefaultValue(false)]
    public bool? IsDefault { get => fields.IsDefault[this]; set => fields.IsDefault[this] = value; }
    public partial class RowFields { public BooleanField IsDefault; }

    [DisplayName("Aktif"), NotNull, DefaultValue(true)]
    public bool? IsActive { get => fields.IsActive[this]; set => fields.IsActive[this] = value; }
    public partial class RowFields { public BooleanField IsActive; }

    [DisplayName("Maks. Tekrar Sayısı"), NotNull, DefaultValue(3)]
    public int? MaxRetryCount { get => fields.MaxRetryCount[this]; set => fields.MaxRetryCount[this] = value; }
    public partial class RowFields { public Int32Field MaxRetryCount; }

    [DisplayName("Tekrar Aralığı (dk)"), NotNull, DefaultValue(15)]
    public int? RetryIntervalMinutes { get => fields.RetryIntervalMinutes[this]; set => fields.RetryIntervalMinutes[this] = value; }
    public partial class RowFields { public Int32Field RetryIntervalMinutes; }

    [DisplayName("Günlük Limit")]
    public int? DailyLimit { get => fields.DailyLimit[this]; set => fields.DailyLimit[this] = value; }
    public partial class RowFields { public Int32Field DailyLimit; }

    [DisplayName("Oluşturma Tarihi"), Insertable(false), Updatable(false)]
    public DateTime? InsertDate { get => fields.InsertDate[this]; set => fields.InsertDate[this] = value; }
    public partial class RowFields { public DateTimeField InsertDate; }

    [DisplayName("Oluşturan"), Insertable(false), Updatable(false)]
    public int? InsertUserId { get => fields.InsertUserId[this]; set => fields.InsertUserId[this] = value; }
    public partial class RowFields { public Int32Field InsertUserId; }

    [DisplayName("Güncelleme Tarihi"), Insertable(false), Updatable(false)]
    public DateTime? UpdateDate { get => fields.UpdateDate[this]; set => fields.UpdateDate[this] = value; }
    public partial class RowFields { public DateTimeField UpdateDate; }

    [DisplayName("Güncelleyen"), Insertable(false), Updatable(false)]
    public int? UpdateUserId { get => fields.UpdateUserId[this]; set => fields.UpdateUserId[this] = value; }
    public partial class RowFields { public Int32Field UpdateUserId; }

    public partial class RowFields : RowFieldsBase { }
}
