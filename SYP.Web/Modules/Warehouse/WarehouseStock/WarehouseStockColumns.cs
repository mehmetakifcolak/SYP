namespace SYP.Warehouse.Columns;

[ColumnsScript("Warehouse.WarehouseStock")]
[BasedOnRow(typeof(WarehouseStockRow), CheckNames = true)]
public class WarehouseStockColumns
{
    // Depo ve ürün için açılır liste (lookup) quick filter'ları. Bu alanlar gridde
    // gizlidir, yalnızca filtre çubuğunda görünür. Düz metin string QuickFilter'ı
    // Serenity 9.1.1 corelib'inde hata verdiğinden filtreleme lookup üzerinden yapılır.
    [QuickFilter, Visible(false)]
    public int? WarehouseId { get; set; }

    [QuickFilter, Visible(false)]
    public int? ProductId { get; set; }

    [Width(120)]
    public string WarehouseCode { get; set; }

    [Width(150)]
    public string WarehouseName { get; set; }

    [Width(120)]
    public string ProductCode { get; set; }

    [Width(250)]
    public string ProductName { get; set; }

    [Width(120), AlignRight]
    public decimal Quantity { get; set; }

    [Width(150)]
    public DateTime LastUpdateDate { get; set; }
}
