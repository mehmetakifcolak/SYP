namespace SYP.Catalog.Columns;

[ColumnsScript("Catalog.Brands")]
[BasedOnRow(typeof(BrandsRow), CheckNames = true)]
public class BrandsColumns
{
    [EditLink, Width(100)]
    public string Code { get; set; }

    [EditLink, Width(200)]
    public string Name { get; set; }

    [Width(80)]
    public string Logo { get; set; }

    [Width(80)]
    public bool IsActive { get; set; }

    [Width(80)]
    public int SortOrder { get; set; }
}
