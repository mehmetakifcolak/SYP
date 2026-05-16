using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[Migration(20260516_1900)]
public class DefaultDB_20260516_1900_StockExits : AutoReversingMigration
{
    public override void Up()
    {
        // Stok Çıkışları Ana Tablo
        Create.Table("StockExits")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("ExitNo").AsString(50).NotNullable()
            .WithColumn("WarehouseId").AsInt32().NotNullable()
                .ForeignKey("FK_StockExits_Warehouse", "Warehouses", "Id")
            .WithColumn("ExitDate").AsDateTime().NotNullable()
            .WithColumn("Description").AsString(500).Nullable()
            .WithColumn("Status").AsInt16().NotNullable().WithDefaultValue(0)
            .WithColumn("InsertDate").AsDateTime().Nullable()
            .WithColumn("InsertUserId").AsInt32().Nullable()
            .WithColumn("UpdateDate").AsDateTime().Nullable()
            .WithColumn("UpdateUserId").AsInt32().Nullable();

        Create.Index("IX_StockExits_ExitNo")
            .OnTable("StockExits")
            .OnColumn("ExitNo").Unique();

        Create.Index("IX_StockExits_WarehouseId")
            .OnTable("StockExits")
            .OnColumn("WarehouseId");

        // Stok Çıkışları Detay Tablo
        Create.Table("StockExitDetails")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("StockExitId").AsInt32().NotNullable()
                .ForeignKey("FK_StockExitDetails_StockExit", "StockExits", "Id")
            .WithColumn("ProductId").AsInt32().NotNullable()
                .ForeignKey("FK_StockExitDetails_Product", "Products", "Id")
            .WithColumn("Quantity").AsDecimal(18, 4).NotNullable()
            .WithColumn("Notes").AsString(500).Nullable();

        Create.Index("IX_StockExitDetails_StockExitId")
            .OnTable("StockExitDetails")
            .OnColumn("StockExitId");
    }
}
