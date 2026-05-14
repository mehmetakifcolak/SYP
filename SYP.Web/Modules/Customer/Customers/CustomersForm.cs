using Serenity.ComponentModel;

namespace SYP.Customer.Forms;

[FormScript("Customer.Customers")]
[BasedOnRow(typeof(CustomersRow), CheckNames = true)]
public class CustomersForm
{
    [Placeholder("Boş bırakılırsa otomatik oluşturulur")]
    public string Code { get; set; }

    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }

    [DefaultValue(true)]
    public bool IsActive { get; set; }
}