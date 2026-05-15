namespace SYP.Administration;

[ConnectionKey("Default"), Module("Administration"), TableName("AuditLog")]
[DisplayName("Islem Loglari"), InstanceName("Islem Logu")]
[NavigationPermission("Administration:AuditLog:Navigation")]
[ReadPermission("Administration:AuditLog:Read")]
public sealed class AuditLogRow : Row<AuditLogRow.RowFields>, IIdRow, INameRow
{
    const string jUser = nameof(jUser);

    [DisplayName("Id"), Identity, IdProperty]
    public long? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int64Field Id; }

    [DisplayName("Tablo"), Size(100), NotNull, QuickSearch, QuickFilter]
    public string EntityType { get => fields.EntityType[this]; set => fields.EntityType[this] = value; }
    public partial class RowFields { public StringField EntityType; }

    [DisplayName("Kayit ID"), Size(50), NotNull]
    public string EntityId { get => fields.EntityId[this]; set => fields.EntityId[this] = value; }
    public partial class RowFields { public StringField EntityId; }

    [DisplayName("Kayit Adi"), Size(500), NameProperty]
    public string EntityName { get => fields.EntityName[this]; set => fields.EntityName[this] = value; }
    public partial class RowFields { public StringField EntityName; }

    [DisplayName("Islem Tipi"), Size(20), NotNull, QuickFilter]
    public string ActionType { get => fields.ActionType[this]; set => fields.ActionType[this] = value; }
    public partial class RowFields { public StringField ActionType; }

    [DisplayName("Islem Tarihi"), NotNull, QuickFilter]
    public DateTime? ActionDate { get => fields.ActionDate[this]; set => fields.ActionDate[this] = value; }
    public partial class RowFields { public DateTimeField ActionDate; }

    [DisplayName("Kullanici"), ForeignKey("Users", "UserId"), LeftJoin(jUser), QuickFilter]
    [LookupEditor(typeof(UserRow))]
    public int? UserId { get => fields.UserId[this]; set => fields.UserId[this] = value; }
    public partial class RowFields { public Int32Field UserId; }

    [DisplayName("Kullanici Adi"), Size(100)]
    public string Username { get => fields.Username[this]; set => fields.Username[this] = value; }
    public partial class RowFields { public StringField Username; }

    [DisplayName("IP Adresi"), Size(50)]
    public string IpAddress { get => fields.IpAddress[this]; set => fields.IpAddress[this] = value; }
    public partial class RowFields { public StringField IpAddress; }

    [DisplayName("Eski Degerler")]
    public string OldValues { get => fields.OldValues[this]; set => fields.OldValues[this] = value; }
    public partial class RowFields { public StringField OldValues; }

    [DisplayName("Yeni Degerler")]
    public string NewValues { get => fields.NewValues[this]; set => fields.NewValues[this] = value; }
    public partial class RowFields { public StringField NewValues; }

    [DisplayName("Degisen Alanlar"), Size(1000)]
    public string ChangedFields { get => fields.ChangedFields[this]; set => fields.ChangedFields[this] = value; }
    public partial class RowFields { public StringField ChangedFields; }

    #region Foreign Fields

    [DisplayName("Kullanici Tam Adi"), Expression($"{jUser}.[DisplayName]")]
    public string UserDisplayName { get => fields.UserDisplayName[this]; set => fields.UserDisplayName[this] = value; }
    public partial class RowFields { public StringField UserDisplayName; }

    #endregion

    public partial class RowFields : RowFieldsBase { }
}
