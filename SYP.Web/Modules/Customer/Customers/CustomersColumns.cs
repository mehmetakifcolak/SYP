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
    public string Email { get; set; }
    public string Phone { get; set; }

    [DisplayName("Satıcı Tipi"), Width(150)]
    public string VendorTypeTitle { get; set; }

    public bool IsActive { get; set; }

    [DisplayName("Kullanıcı"), Width(150)]
    public string Username { get; set; }

    [DisplayName("K. Durumu"), Width(120)]
    [FormatterType("SYP.Customer.UserStatusFormatter")]
    public short UserIsActive { get; set; }
}