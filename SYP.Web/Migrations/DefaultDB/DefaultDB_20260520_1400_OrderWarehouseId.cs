using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[DefaultDB, MigrationKey(20260520_1400)]
public class DefaultDB_20260520_1400_OrderWarehouseId : AutoReversingMigration
{
    public override void Up()
    {
        if (!Schema.Table("Orders").Column("WarehouseId").Exists())
        {
            Alter.Table("Orders")
                .AddColumn("WarehouseId").AsInt32().Nullable()
                .ForeignKey("FK_Orders_Warehouses", "Warehouses", "Id");
        }
    }
}
