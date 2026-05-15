namespace SYP.Warehouse.Columns;

[ColumnsScript("Warehouse.Warehouses")]
[BasedOnRow(typeof(WarehousesRow), CheckNames = true)]
public class WarehousesColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, Width(80)]
    public int Id { get; set; }

    [EditLink, Width(120)]
    public string Code { get; set; }

    [Width(200)]
    public string Name { get; set; }

    [Width(150)]
    public string City { get; set; }

    [Width(120)]
    public string Phone { get; set; }

    [Width(150)]
    public string ManagerName { get; set; }

    [Width(80)]
    public bool IsActive { get; set; }
}
