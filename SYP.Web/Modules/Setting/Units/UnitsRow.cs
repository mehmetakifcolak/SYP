namespace SYP.Setting;

[ConnectionKey("Default"), Module("Setting"), TableName("Units")]
[DisplayName("Units"), InstanceName("Unit")]
[LookupScript("Setting.Units", Permission = "*")]
[NavigationPermission("Setting:Units:Navigation")]
[ReadPermission("Setting:Units:Read")]
[InsertPermission("Setting:Units:Insert")]
[UpdatePermission("Setting:Units:Update")]
[DeletePermission("Setting:Units:Delete")]
public sealed class UnitsRow : Row<UnitsRow.RowFields>, IIdRow, INameRow
{
    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }

    [DisplayName("Code"), Size(10), NotNull, QuickSearch, LookupInclude]
    public string Code { get => fields.Code[this]; set => fields.Code[this] = value; }
    public partial class RowFields { public StringField Code; }

    [DisplayName("Name"), Size(50), NotNull, NameProperty, LookupInclude]
    public string Name { get => fields.Name[this]; set => fields.Name[this] = value; }
    public partial class RowFields { public StringField Name; }

    [DisplayName("Active"), NotNull, LookupInclude]
    public bool? IsActive { get => fields.IsActive[this]; set => fields.IsActive[this] = value; }
    public partial class RowFields { public BooleanField IsActive; }

    [DisplayName("Sort Order"), NotNull]
    public int? SortOrder { get => fields.SortOrder[this]; set => fields.SortOrder[this] = value; }
    public partial class RowFields { public Int32Field SortOrder; }

    public partial class RowFields : RowFieldsBase { }
}
