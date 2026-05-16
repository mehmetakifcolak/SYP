using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[Migration(20260516_2000)]
public class DefaultDB_20260516_2000_StockMovements : Migration
{
    public override void Up()
    {
        // Stok Hareketleri View - Giriş ve Çıkışları birleştirir
        // Id olarak detay tablosunun Id'si kullanılır (unique olması için)
        Execute.Sql(@"
            CREATE VIEW StockMovements AS
            SELECT
                CONCAT('E', sed.Id) AS Id,
                'Entry' AS MovementType,
                se.EntryNo AS DocumentNo,
                se.WarehouseId,
                w.Code AS WarehouseCode,
                w.Name AS WarehouseName,
                sed.ProductId,
                p.Code AS ProductCode,
                p.Name AS ProductName,
                sed.Quantity AS Quantity,
                se.EntryDate AS MovementDate,
                se.Status,
                se.Description,
                se.InsertDate,
                se.InsertUserId
            FROM StockEntries se
            INNER JOIN StockEntryDetails sed ON sed.StockEntryId = se.Id
            INNER JOIN Warehouses w ON w.Id = se.WarehouseId
            INNER JOIN Products p ON p.Id = sed.ProductId

            UNION ALL

            SELECT
                CONCAT('X', sxd.Id) AS Id,
                'Exit' AS MovementType,
                sx.ExitNo AS DocumentNo,
                sx.WarehouseId,
                w.Code AS WarehouseCode,
                w.Name AS WarehouseName,
                sxd.ProductId,
                p.Code AS ProductCode,
                p.Name AS ProductName,
                -sxd.Quantity AS Quantity,
                sx.ExitDate AS MovementDate,
                sx.Status,
                sx.Description,
                sx.InsertDate,
                sx.InsertUserId
            FROM StockExits sx
            INNER JOIN StockExitDetails sxd ON sxd.StockExitId = sx.Id
            INNER JOIN Warehouses w ON w.Id = sx.WarehouseId
            INNER JOIN Products p ON p.Id = sxd.ProductId
        ");
    }

    public override void Down()
    {
        Execute.Sql("DROP VIEW IF EXISTS StockMovements");
    }
}
