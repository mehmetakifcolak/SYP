using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[Migration(20260515_1800)]
public class DefaultDB_20260515_1700_StockEntries : AutoReversingMigration
{
    public override void Up()
    {
        // Stok Girişleri Ana Tablo
        Create.Table("StockEntries")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("EntryNo").AsString(50).NotNullable()
            .WithColumn("WarehouseId").AsInt32().NotNullable()
                .ForeignKey("FK_StockEntries_Warehouse", "Warehouses", "Id")
            .WithColumn("EntryDate").AsDateTime().NotNullable()
            .WithColumn("Description").AsString(500).Nullable()
            .WithColumn("Status").AsInt16().NotNullable().WithDefaultValue(0)
            .WithColumn("InsertDate").AsDateTime().Nullable()
            .WithColumn("InsertUserId").AsInt32().Nullable()
            .WithColumn("UpdateDate").AsDateTime().Nullable()
            .WithColumn("UpdateUserId").AsInt32().Nullable();

        Create.Index("IX_StockEntries_EntryNo")
            .OnTable("StockEntries")
            .OnColumn("EntryNo").Unique();

        Create.Index("IX_StockEntries_WarehouseId")
            .OnTable("StockEntries")
            .OnColumn("WarehouseId");

        // Stok Girişleri Detay Tablo
        Create.Table("StockEntryDetails")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("StockEntryId").AsInt32().NotNullable()
                .ForeignKey("FK_StockEntryDetails_StockEntry", "StockEntries", "Id")
            .WithColumn("ProductId").AsInt32().NotNullable()
                .ForeignKey("FK_StockEntryDetails_Product", "Products", "Id")
            .WithColumn("Quantity").AsDecimal(18, 4).NotNullable()
            .WithColumn("Notes").AsString(500).Nullable();

        Create.Index("IX_StockEntryDetails_StockEntryId")
            .OnTable("StockEntryDetails")
            .OnColumn("StockEntryId");

        // Depo Stok Durumu Tablosu
        Create.Table("WarehouseStock")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("WarehouseId").AsInt32().NotNullable()
                .ForeignKey("FK_WarehouseStock_Warehouse", "Warehouses", "Id")
            .WithColumn("ProductId").AsInt32().NotNullable()
                .ForeignKey("FK_WarehouseStock_Product", "Products", "Id")
            .WithColumn("Quantity").AsDecimal(18, 4).NotNullable().WithDefaultValue(0)
            .WithColumn("LastUpdateDate").AsDateTime().Nullable();

        Create.UniqueConstraint("UQ_WarehouseStock_Warehouse_Product")
            .OnTable("WarehouseStock")
            .Columns("WarehouseId", "ProductId");
    }
}
