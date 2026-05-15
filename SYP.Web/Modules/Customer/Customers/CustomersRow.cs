using SYP.Administration;

namespace SYP.Customer;

[ConnectionKey("Default"), Module("Customer"), TableName("Customers")]
[DisplayName("Customers"), InstanceName("Customers")]
[NavigationPermission("Customer:Customers:Navigation")]
[ReadPermission("Customer:Customers:Read")]
[InsertPermission("Customer:Customers:Insert")]
[UpdatePermission("Customer:Customers:Update")]
[DeletePermission("Customer:Customers:Delete")]
[ServiceLookupPermission("Customer:Customers:Lookup")]
public sealed class CustomersRow : Row<CustomersRow.RowFields>, IIdRow, INameRow, IAuditedRow
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

    [DisplayName("Vendor Type"), ForeignKey("[dbo].[VendorType]", "Id"), LeftJoin("jVendorType")]
    [LookupEditor("Setting.VendorType")]
    public int? VendorTypeId { get => fields.VendorTypeId[this]; set => fields.VendorTypeId[this] = value; }
    public partial class RowFields { public Int32Field VendorTypeId; }
    
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

    [DisplayName("User Id"), ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jUser")]
    public int? UserId { get => fields.UserId[this]; set => fields.UserId[this] = value; }
    public partial class RowFields { public Int32Field UserId; }

    [DisplayName("Password"), Size(50), NotMapped]
    public string Password { get => fields.Password[this]; set => fields.Password[this] = value; }
    public partial class RowFields { public StringField Password; }

    [DisplayName("Password Confirm"), Size(50), NotMapped]
    public string PasswordConfirm { get => fields.PasswordConfirm[this]; set => fields.PasswordConfirm[this] = value; }
    public partial class RowFields { public StringField PasswordConfirm; }

    #region Foreign Fields

    [DisplayName("Kullanıcı Adı"), Expression("jUser.[Username]")]
    public string Username { get => fields.Username[this]; set => fields.Username[this] = value; }
    public partial class RowFields { public StringField Username; }

    [DisplayName("Satıcı Tipi"), Expression("jVendorType.[Title]")]
    public string VendorTypeTitle { get => fields.VendorTypeTitle[this]; set => fields.VendorTypeTitle[this] = value; }
    public partial class RowFields { public StringField VendorTypeTitle; }

    [DisplayName("Kullanıcı Aktif"), Expression("jUser.[IsActive]")]
    public short? UserIsActive { get => fields.UserIsActive[this]; set => fields.UserIsActive[this] = value; }
    public partial class RowFields { public Int16Field UserIsActive; }

    #endregion Foreign Fields

    public partial class RowFields : RowFieldsBase { }
}