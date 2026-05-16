namespace SYP.Products.Columns;

[ColumnsScript("Products.BrandsEditor")]
[BasedOnRow(typeof(BrandsRow), CheckNames = true)]
public class BrandsEditorColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
    public int Id { get; set; }
    [EditLink]
    public string Name { get; set; }
    public string Description { get; set; }
    public string Logo { get; set; }
    public bool IsActive { get; set; }
    public DateTime InsertDate { get; set; }
    public int InsertUserId { get; set; }
    public DateTime UpdateDate { get; set; }
    public int UpdateUserId { get; set; }
}