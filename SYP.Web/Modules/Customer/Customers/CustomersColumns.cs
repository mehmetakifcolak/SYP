namespace SYP.Customer.Columns;

[ColumnsScript("Customer.Customers")]
[BasedOnRow(typeof(CustomersRow), CheckNames = true)]
public class CustomersColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
    public int Id { get; set; }
    [EditLink]
    public string Code { get; set; }
    public string Name { get; set; }

    [DisplayName("Ad")]
    public string FirstName { get; set; }

    [DisplayName("Soyad")]
    public string LastName { get; set; }

    public string Email { get; set; }
    public string Phone { get; set; }

    [DisplayName("Telefon 2")]
    public string Phone2 { get; set; }

    [DisplayName("Ülke"), Width(120)]
    public string CountryName { get; set; }

    [DisplayName("Şehir")]
    public string City { get; set; }

    [DisplayName("İlçe")]
    public string District { get; set; }

    [DisplayName("Vergi Dairesi")]
    public string TaxOffice { get; set; }

    [DisplayName("Vergi No")]
    public string TaxNumber { get; set; }

    [DisplayName("Satıcı Tipi"), Width(150)]
    public string VendorTypeTitle { get; set; }

    [DisplayName("Para Birimi"), Width(100), AlignCenter]
    public string CurrencyCode { get; set; }

    public bool IsActive { get; set; } 

    [DisplayName("K. Durumu"), Width(120)]
    [FormatterType("SYP.Customer.UserStatusFormatter")]
    public short UserIsActive { get; set; }
}