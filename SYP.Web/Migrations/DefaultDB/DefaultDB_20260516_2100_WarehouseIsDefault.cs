using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[Migration(20260516_2100)]
public class DefaultDB_20260516_2100_WarehouseIsDefault : AutoReversingMigration
{
    public override void Up()
    {
        Alter.Table("Warehouses")
            .AddColumn("IsDefault").AsBoolean().NotNullable().WithDefaultValue(false);
    }
}
