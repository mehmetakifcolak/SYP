namespace SYP.Customer.Forms;

[FormScript("Customer.CustomersEditor")]
[BasedOnRow(typeof(CustomersRow), CheckNames = true)]
public class CustomersEditorForm
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public bool IsActive { get; set; }
    public DateTime InsertDate { get; set; }
    public int InsertUserId { get; set; }
    public DateTime UpdateDate { get; set; }
    public int UpdateUserId { get; set; }
}