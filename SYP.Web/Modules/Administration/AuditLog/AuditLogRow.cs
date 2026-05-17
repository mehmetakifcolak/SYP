namespace SYP.Administration;

[ConnectionKey("Default"), Module("Administration"), TableName("AuditLog")]
[DisplayName("Audit Logs"), InstanceName("Audit Log")]
[NavigationPermission("Administration:AuditLog:Navigation")]
[ReadPermission("Administration:AuditLog:Read")]
public sealed class AuditLogRow : Row<AuditLogRow.RowFields>, IIdRow, INameRow
{
    const string jUser = nameof(jUser);

    [DisplayName("Id"), Identity, IdProperty]
    public long? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int64Field Id; }

    [DisplayName("Entity Type"), Size(100), NotNull, QuickSearch, QuickFilter]
    public string EntityType { get => fields.EntityType[this]; set => fields.EntityType[this] = value; }
    public partial class RowFields { public StringField EntityType; }

    [DisplayName("Entity ID"), Size(50), NotNull]
    public string EntityId { get => fields.EntityId[this]; set => fields.EntityId[this] = value; }
    public partial class RowFields { public StringField EntityId; }

    [DisplayName("Entity Name"), Size(500), NameProperty]
    public string EntityName { get => fields.EntityName[this]; set => fields.EntityName[this] = value; }
    public partial class RowFields { public StringField EntityName; }

    [DisplayName("Action Type"), Size(20), NotNull, QuickFilter]
    public string ActionType { get => fields.ActionType[this]; set => fields.ActionType[this] = value; }
    public partial class RowFields { public StringField ActionType; }

    [DisplayName("Action Date"), NotNull, QuickFilter]
    public DateTime? ActionDate { get => fields.ActionDate[this]; set => fields.ActionDate[this] = value; }
    public partial class RowFields { public DateTimeField ActionDate; }

    [DisplayName("User"), ForeignKey("Users", "UserId"), LeftJoin(jUser), QuickFilter]
    [LookupEditor(typeof(UserRow))]
    public int? UserId { get => fields.UserId[this]; set => fields.UserId[this] = value; }
    public partial class RowFields { public Int32Field UserId; }

    [DisplayName("Username"), Size(100)]
    public string Username { get => fields.Username[this]; set => fields.Username[this] = value; }
    public partial class RowFields { public StringField Username; }

    [DisplayName("IP Address"), Size(50)]
    public string IpAddress { get => fields.IpAddress[this]; set => fields.IpAddress[this] = value; }
    public partial class RowFields { public StringField IpAddress; }

    [DisplayName("Old Values")]
    public string OldValues { get => fields.OldValues[this]; set => fields.OldValues[this] = value; }
    public partial class RowFields { public StringField OldValues; }

    [DisplayName("New Values")]
    public string NewValues { get => fields.NewValues[this]; set => fields.NewValues[this] = value; }
    public partial class RowFields { public StringField NewValues; }

    [DisplayName("Changed Fields"), Size(1000)]
    public string ChangedFields { get => fields.ChangedFields[this]; set => fields.ChangedFields[this] = value; }
    public partial class RowFields { public StringField ChangedFields; }

    #region Foreign Fields

    [DisplayName("User Display Name"), Expression($"{jUser}.[DisplayName]")]
    public string UserDisplayName { get => fields.UserDisplayName[this]; set => fields.UserDisplayName[this] = value; }
    public partial class RowFields { public StringField UserDisplayName; }

    #endregion

    public partial class RowFields : RowFieldsBase { }
}
