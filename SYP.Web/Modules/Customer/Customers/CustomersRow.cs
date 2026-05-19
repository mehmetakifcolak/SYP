using SYP.Administration;

namespace SYP.Customer;

[ConnectionKey("Default"), Module("Customer"), TableName("Customers")]
[DisplayName("Customers"), InstanceName("Customers")]
[ReadPermission("Customer:Customers:Read")]
[InsertPermission("Customer:Customers:Insert")]
[UpdatePermission("Customer:Customers:Update")]
[DeletePermission("Customer:Customers:Delete")]
[NavigationPermission("Customer:Customers:Navigation")]
[LookupScript("Customer.Customers", Permission = "Customer:Customers:Read")]
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

    [DisplayName("First Name"), Size(1000)]
    public string FirstName { get => fields.FirstName[this]; set => fields.FirstName[this] = value; }
    public partial class RowFields { public StringField FirstName; }

    [DisplayName("Last Name"), Size(300)]
    public string LastName { get => fields.LastName[this]; set => fields.LastName[this] = value; }
    public partial class RowFields { public StringField LastName; }

    [DisplayName("Email"), Size(200)]
    public string Email { get => fields.Email[this]; set => fields.Email[this] = value; }
    public partial class RowFields { public StringField Email; }

    [DisplayName("Phone"), Size(50)]
    public string Phone { get => fields.Phone[this]; set => fields.Phone[this] = value; }
    public partial class RowFields { public StringField Phone; }

    [DisplayName("Phone 2"), Size(50)]
    public string Phone2 { get => fields.Phone2[this]; set => fields.Phone2[this] = value; }
    public partial class RowFields { public StringField Phone2; }

    [DisplayName("Address"), Size(4000)]
    public string Address { get => fields.Address[this]; set => fields.Address[this] = value; }
    public partial class RowFields { public StringField Address; }

    [DisplayName("Country"), ForeignKey("[dbo].[Country]", "Id"), LeftJoin("jCountry")]
    [LookupEditor("Setting.Country")]
    public int? CountryId { get => fields.CountryId[this]; set => fields.CountryId[this] = value; }
    public partial class RowFields { public Int32Field CountryId; }

    [DisplayName("City"), Size(100)]
    public string City { get => fields.City[this]; set => fields.City[this] = value; }
    public partial class RowFields { public StringField City; }

    [DisplayName("District"), Size(100)]
    public string District { get => fields.District[this]; set => fields.District[this] = value; }
    public partial class RowFields { public StringField District; }

    [DisplayName("Tax Office"), Size(100)]
    public string TaxOffice { get => fields.TaxOffice[this]; set => fields.TaxOffice[this] = value; }
    public partial class RowFields { public StringField TaxOffice; }

    [DisplayName("Tax Number"), Size(100)]
    public string TaxNumber { get => fields.TaxNumber[this]; set => fields.TaxNumber[this] = value; }
    public partial class RowFields { public StringField TaxNumber; }
    
    [DisplayName("Is Active")]
    public bool? IsActive { get => fields.IsActive[this]; set => fields.IsActive[this] = value; }
    public partial class RowFields { public BooleanField IsActive; }

    [DisplayName("Vendor Type"), ForeignKey("[dbo].[VendorType]", "Id"), LeftJoin("jVendorType"), LookupInclude]
    [LookupEditor("Setting.VendorType")]
    public int? VendorTypeId { get => fields.VendorTypeId[this]; set => fields.VendorTypeId[this] = value; }
    public partial class RowFields { public Int32Field VendorTypeId; }

    [DisplayName("Para Birimi"), ForeignKey("[dbo].[CurrencyList]", "Id"), LeftJoin("jCurrency")]
    [LookupEditor("Setting.CurrencyList")]
    public int? CurrencyId { get => fields.CurrencyId[this]; set => fields.CurrencyId[this] = value; }
    public partial class RowFields { public Int32Field CurrencyId; }

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

    [DisplayName("Sorumlu Yönetici"), ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jManager")]
    [LookupEditor("Administration.User")]
    public int? ManagerUserId { get => fields.ManagerUserId[this]; set => fields.ManagerUserId[this] = value; }
    public partial class RowFields { public Int32Field ManagerUserId; }

    [DisplayName("Password"), Size(50), NotMapped]
    public string Password { get => fields.Password[this]; set => fields.Password[this] = value; }
    public partial class RowFields { public StringField Password; }

    [DisplayName("Password Confirm"), Size(50), NotMapped]
    public string PasswordConfirm { get => fields.PasswordConfirm[this]; set => fields.PasswordConfirm[this] = value; }
    public partial class RowFields { public StringField PasswordConfirm; }

    #region Foreign Fields

    [DisplayName("Vendor Type Title"), Expression("jVendorType.[Title]")]
    public string VendorTypeTitle { get => fields.VendorTypeTitle[this]; set => fields.VendorTypeTitle[this] = value; }
    public partial class RowFields { public StringField VendorTypeTitle; }

    [DisplayName("User Is Active"), Expression("jUser.[IsActive]")]
    public short? UserIsActive { get => fields.UserIsActive[this]; set => fields.UserIsActive[this] = value; }
    public partial class RowFields { public Int16Field UserIsActive; }

    [DisplayName("Country Name"), Expression("jCountry.[Name]")]
    public string CountryName { get => fields.CountryName[this]; set => fields.CountryName[this] = value; }
    public partial class RowFields { public StringField CountryName; }

    [DisplayName("Yönetici Adı"), Expression("jManager.[DisplayName]")]
    public string ManagerName { get => fields.ManagerName[this]; set => fields.ManagerName[this] = value; }
    public partial class RowFields { public StringField ManagerName; }

    [DisplayName("Para Birimi Kodu"), Expression("jCurrency.[Code]")]
    public string CurrencyCode { get => fields.CurrencyCode[this]; set => fields.CurrencyCode[this] = value; }
    public partial class RowFields { public StringField CurrencyCode; }

    #endregion Foreign Fields

    public partial class RowFields : RowFieldsBase { }
}