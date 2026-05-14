namespace SYP.Customer;

[ConnectionKey("Default"), Module("Customer"), TableName("Customers")]
[DisplayName("Customers"), InstanceName("Customers")]
[NavigationPermission("Customer:Customers:Navigation")]
[ReadPermission("Customer:Customers:Read")]
[InsertPermission("Customer:Customers:Insert")]
[UpdatePermission("Customer:Customers:Update")]
[DeletePermission("Customer:Customers:Delete")]
[ServiceLookupPermission("Customer:Customers:Lookup")]
public sealed class CustomersRow : Row<CustomersRow.RowFields>, IIdRow, INameRow
{
    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }
    
    [DisplayName("Code"), Size(50), QuickSearch, NameProperty]
    public string Code { get => fields.Code[this]; set => fields.Code[this] = value; }
    public partial class RowFields { public StringField Code; }
    
    [DisplayName("Name"), Size(300)]
    public string Name { get => fields.Name[this]; set => fields.Name[this] = value; }
    public partial class RowFields { public StringField Name; }
    
    [DisplayName("Email"), Size(200)]
    public string Email { get => fields.Email[this]; set => fields.Email[this] = value; }
    public partial class RowFields { public StringField Email; }
    
    [DisplayName("Phone"), Size(50)]
    public string Phone { get => fields.Phone[this]; set => fields.Phone[this] = value; }
    public partial class RowFields { public StringField Phone; }
    
    [DisplayName("Address"), Size(500)]
    public string Address { get => fields.Address[this]; set => fields.Address[this] = value; }
    public partial class RowFields { public StringField Address; }
    
    [DisplayName("Is Active")]
    public bool? IsActive { get => fields.IsActive[this]; set => fields.IsActive[this] = value; }
    public partial class RowFields { public BooleanField IsActive; }
    
    [DisplayName("Insert Date")]
    public DateTime? InsertDate { get => fields.InsertDate[this]; set => fields.InsertDate[this] = value; }
    public partial class RowFields { public DateTimeField InsertDate; }
    
    [DisplayName("Insert User Id")]
    public int? InsertUserId { get => fields.InsertUserId[this]; set => fields.InsertUserId[this] = value; }
    public partial class RowFields { public Int32Field InsertUserId; }
    
    [DisplayName("Update Date")]
    public DateTime? UpdateDate { get => fields.UpdateDate[this]; set => fields.UpdateDate[this] = value; }
    public partial class RowFields { public DateTimeField UpdateDate; }
    
    [DisplayName("Update User Id")]
    public int? UpdateUserId { get => fields.UpdateUserId[this]; set => fields.UpdateUserId[this] = value; }
    public partial class RowFields { public Int32Field UpdateUserId; }
    
    #region Foreign Fields

    #endregion Foreign Fields

    public partial class RowFields : RowFieldsBase { }
}