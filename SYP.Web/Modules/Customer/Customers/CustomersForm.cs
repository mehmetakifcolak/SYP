using Serenity.ComponentModel;

namespace SYP.Customer.Forms;

[FormScript("Customer.Customers")]
[BasedOnRow(typeof(CustomersRow), CheckNames = true)]
public class CustomersForm
{
    [Tab("Genel Bilgiler")]
    [Category("Temel Bilgiler")]
    [HalfWidth, Placeholder("Boş bırakılırsa otomatik oluşturulur")]
    public string Code { get; set; }

    [HalfWidth, DisplayName("Satıcı Tipi")]
    public int? VendorTypeId { get; set; }

    [HalfWidth, DisplayName("Sorumlu Yönetici")]
    public int? ManagerUserId { get; set; }

    [HalfWidth, DisplayName("Firma/Müşteri Adı")]
    public string Name { get; set; }

    [HalfWidth, DefaultValue(true), DisplayName("Aktif")]
    public bool IsActive { get; set; }

    [HalfWidth, DisplayName("Ad")]
    public string FirstName { get; set; }

    [HalfWidth, DisplayName("Soyad")]
    public string LastName { get; set; }

    [Category("İletişim Bilgileri")]
    [HalfWidth, DisplayName("Telefon")]
    public string Phone { get; set; }

    [HalfWidth, DisplayName("Telefon 2")]
    public string Phone2 { get; set; }

    [HalfWidth, EmailAddressEditor, DisplayName("E-posta")]
    public string Email { get; set; }

    [Category("Adres Bilgileri")]
    [ThreeQuarterWidth, TextAreaEditor(Rows = 2), DisplayName("Adres")]
    public string Address { get; set; }

    [HalfWidth, DisplayName("Ülke")]
    public int? CountryId { get; set; }

    [HalfWidth, DisplayName("Şehir")]
    public string City { get; set; }

    [HalfWidth, DisplayName("İlçe")]
    public string District { get; set; }

    [Tab("Vergi & Hesap")]
    [Category("Vergi Bilgileri")]
    [HalfWidth, DisplayName("Vergi Dairesi")]
    public string TaxOffice { get; set; }

    [HalfWidth, DisplayName("Vergi Numarası")]
    public string TaxNumber { get; set; }

    [Category("Kullanıcı Hesabı")]
    [HalfWidth, DisplayName("Kullanıcı ID"), ReadOnly(true), HideOnInsert]
    public int? UserId { get; set; }

    [HalfWidth, DisplayName("Kullanıcı Adı"), ReadOnly(true), HideOnInsert]
    public string Username { get; set; }

    [HalfWidth, PasswordEditor, Placeholder("Min 6 karakter"), DisplayName("Şifre")]
    public string Password { get; set; }

    [HalfWidth, PasswordEditor, Placeholder("Şifre tekrarı"), DisplayName("Şifre Tekrarı")]
    public string PasswordConfirm { get; set; }
}