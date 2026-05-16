namespace SYP.Products.Forms;

[FormScript("Products.BrandsEditor")]
[BasedOnRow(typeof(BrandsRow), CheckNames = true)]
public class BrandsEditorForm
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Logo { get; set; }
    public bool IsActive { get; set; }
    public DateTime InsertDate { get; set; }
    public int InsertUserId { get; set; }
    public DateTime UpdateDate { get; set; }
    public int UpdateUserId { get; set; }
}