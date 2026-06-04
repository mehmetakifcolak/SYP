using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[DefaultDB, MigrationKey(20260604_1000)]
public class DefaultDB_20260604_1000_WarehouseStockView : Migration
{
    public override void Up()
    {
        // WarehouseStock artık fiziksel tabloda tutulmuyor; stok durumu
        // onaylı stok giriş ve çıkış hareketlerinden dinamik olarak hesaplanır.
        // (Status = 1 => Onaylandı)
        Execute.Sql(@"
CREATE VIEW [dbo].[WarehouseStockView] AS
SELECT
    ROW_NUMBER() OVER (ORDER BY t.WarehouseId, t.ProductId) AS [Id],
    t.WarehouseId,
    t.ProductId,
    SUM(t.Quantity)        AS [Quantity],
    MAX(t.MovementDate)    AS [LastUpdateDate]
FROM (
    SELECT se.WarehouseId, sed.ProductId,
           sed.Quantity AS Quantity, se.EntryDate AS MovementDate
    FROM [dbo].[StockEntryDetails] sed
    INNER JOIN [dbo].[StockEntries] se ON se.Id = sed.StockEntryId
    WHERE se.Status = 1
    UNION ALL
    SELECT sx.WarehouseId, sxd.ProductId,
           -sxd.Quantity AS Quantity, sx.ExitDate AS MovementDate
    FROM [dbo].[StockExitDetails] sxd
    INNER JOIN [dbo].[StockExits] sx ON sx.Id = sxd.StockExitId
    WHERE sx.Status = 1
) t
GROUP BY t.WarehouseId, t.ProductId;");
    }

    public override void Down()
    {
        Execute.Sql("DROP VIEW IF EXISTS [dbo].[WarehouseStockView];");
    }
}
