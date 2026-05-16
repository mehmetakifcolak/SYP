namespace SYP.Warehouse.Forms;

[FormScript("Warehouse.Warehouses")]
[BasedOnRow(typeof(WarehousesRow), CheckNames = true)]
public class WarehousesForm
{
    [HalfWidth]
    public string Code { get; set; }

    [HalfWidth]
    public string Name { get; set; }

    [TextAreaEditor(Rows = 3)]
    public string Address { get; set; }

    [HalfWidth]
    public string City { get; set; }

    [HalfWidth]
    public string Phone { get; set; }

    [HalfWidth, EmailAddressEditor]
    public string Email { get; set; }

    [HalfWidth]
    public string ManagerName { get; set; }

    [TextAreaEditor(Rows = 3)]
    public string Description { get; set; }

    [HalfWidth]
    public bool IsActive { get; set; }

    [HalfWidth]
    public bool IsDefault { get; set; }
}
