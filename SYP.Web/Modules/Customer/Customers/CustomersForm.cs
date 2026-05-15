using Serenity.ComponentModel;

namespace SYP.Customer.Forms;

[FormScript("Customer.Customers")]
[BasedOnRow(typeof(CustomersRow), CheckNames = true)]
public class CustomersForm
{
    [Placeholder("Boş bırakılırsa otomatik oluşturulur")]
    public string Code { get; set; }

    public string Name { get; set; }

    [EmailAddressEditor]
    public string Email { get; set; }

    public string Phone { get; set; }
    public string Address { get; set; }

    [DisplayName("Satıcı Tipi")]
    public int? VendorTypeId { get; set; }

    [DefaultValue(true)]
    public bool IsActive { get; set; }

    [Category("Kullanıcı Hesabı")]
    [DisplayName("Kullanıcı ID"), ReadOnly(true), HideOnInsert]
    public int? UserId { get; set; }

    [DisplayName("Kullanıcı Adı"), ReadOnly(true), HideOnInsert]
    public string Username { get; set; }

    [PasswordEditor, Placeholder("Min 6 karakter")]
    [DisplayName("Şifre")]
    public string Password { get; set; }

    [PasswordEditor, Placeholder("Şifre tekrarı")]
    [DisplayName("Şifre Tekrarı")]
    public string PasswordConfirm { get; set; }
}