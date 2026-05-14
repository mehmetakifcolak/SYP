using Serenity.Data;

namespace SYP.Setting;

[ConnectionKey("Default"), Module("Setting"), TableName("NumberTemplates")]
[DisplayName("Number Templates"), InstanceName("Number Templates")]
[NavigationPermission("Setting:NumberTemplates:Navigation")]
[ReadPermission("Setting:NumberTemplates:Read")]
[InsertPermission("Setting:NumberTemplates:Insert")]
[UpdatePermission("Setting:NumberTemplates:Update")]
[DeletePermission("Setting:NumberTemplates:Delete")]
[ServiceLookupPermission("Setting:NumberTemplates:Lookup")]
public sealed class NumberTemplatesRow : Row<NumberTemplatesRow.RowFields>, IIdRow, INameRow
{
    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }
    
    [DisplayName("Prefix"), Size(10), QuickSearch, NameProperty]
    public string Prefix { get => fields.Prefix[this]; set => fields.Prefix[this] = value; }
    public partial class RowFields { public StringField Prefix; }
    
    [DisplayName("Date Format"), Size(50)]
    public string DateFormat { get => fields.DateFormat[this]; set => fields.DateFormat[this] = value; }
    public partial class RowFields { public StringField DateFormat; }
    
    [DisplayName("Suffix"), Size(10)]
    public string Suffix { get => fields.Suffix[this]; set => fields.Suffix[this] = value; }
    public partial class RowFields { public StringField Suffix; }
    
    [DisplayName("Length")]
    public int? Length { get => fields.Length[this]; set => fields.Length[this] = value; }
    public partial class RowFields { public Int32Field Length; }
    
    [DisplayName("Length Text")]
    public int? LengthText { get => fields.LengthText[this]; set => fields.LengthText[this] = value; }
    public partial class RowFields { public Int32Field LengthText; }
    
    [DisplayName("Active"), NotNull]
    public bool? Active { get => fields.Active[this]; set => fields.Active[this] = value; }
    public partial class RowFields { public BooleanField Active; }
    
    [DisplayName("Şablon Tipi")]
    public global::_Ext.NumberTemplateType? Type { get => fields.Type[this]; set => fields.Type[this] = value; }
    public partial class RowFields { public EnumField<global::_Ext.NumberTemplateType> Type; }
    
    [DisplayName("Department Id")]
    public int? DepartmentId { get => fields.DepartmentId[this]; set => fields.DepartmentId[this] = value; }
    public partial class RowFields { public Int32Field DepartmentId; }
    
    [DisplayName("Insert User Id")]
    public int? InsertUserId { get => fields.InsertUserId[this]; set => fields.InsertUserId[this] = value; }
    public partial class RowFields { public Int32Field InsertUserId; }
    
    [DisplayName("Insert Date")]
    public DateTime? InsertDate { get => fields.InsertDate[this]; set => fields.InsertDate[this] = value; }
    public partial class RowFields { public DateTimeField InsertDate; }
    
    [DisplayName("Update User Id")]
    public int? UpdateUserId { get => fields.UpdateUserId[this]; set => fields.UpdateUserId[this] = value; }
    public partial class RowFields { public Int32Field UpdateUserId; }
    
    [DisplayName("Update Date")]
    public DateTime? UpdateDate { get => fields.UpdateDate[this]; set => fields.UpdateDate[this] = value; }
    public partial class RowFields { public DateTimeField UpdateDate; }
    
    [DisplayName("Tenant Id")]
    public int? TenantId { get => fields.TenantId[this]; set => fields.TenantId[this] = value; }
    public partial class RowFields { public Int32Field TenantId; }
    
    #region Foreign Fields

    #endregion Foreign Fields

    public partial class RowFields : RowFieldsBase { }
}