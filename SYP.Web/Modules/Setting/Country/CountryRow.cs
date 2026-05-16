namespace SYP.Setting;

[ConnectionKey("Default"), Module("Setting"), TableName("Country")]
[DisplayName("Country"), InstanceName("Country")]
[NavigationPermission("Setting:Country:Navigation")]
[ReadPermission("Setting:Country:Read")]
[InsertPermission("Setting:Country:Insert")]
[UpdatePermission("Setting:Country:Update")]
[DeletePermission("Setting:Country:Delete")]
[ServiceLookupPermission("Setting:Country:Lookup")]
public sealed class CountryRow : Row<CountryRow.RowFields>, IIdRow, INameRow
{
    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }
    
    [DisplayName("Name"), Size(100), QuickSearch, NameProperty]
    public string Name { get => fields.Name[this]; set => fields.Name[this] = value; }
    public partial class RowFields { public StringField Name; }
    
    [DisplayName("Code"), Size(200)]
    public string Code { get => fields.Code[this]; set => fields.Code[this] = value; }
    public partial class RowFields { public StringField Code; }
    
    [DisplayName("Iso Code3"), Size(100)]
    public string IsoCode3 { get => fields.IsoCode3[this]; set => fields.IsoCode3[this] = value; }
    public partial class RowFields { public StringField IsoCode3; }
    
    [DisplayName("Phone Code"), Size(100)]
    public string PhoneCode { get => fields.PhoneCode[this]; set => fields.PhoneCode[this] = value; }
    public partial class RowFields { public StringField PhoneCode; }
    
    [DisplayName("Country Nr"), Size(100)]
    public string CountryNr { get => fields.CountryNr[this]; set => fields.CountryNr[this] = value; }
    public partial class RowFields { public StringField CountryNr; }
    
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
    
    #region Foreign Fields

    #endregion Foreign Fields

    public partial class RowFields : RowFieldsBase { }
}