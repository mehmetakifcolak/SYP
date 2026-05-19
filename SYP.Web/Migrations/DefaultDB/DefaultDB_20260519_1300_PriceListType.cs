using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[DefaultDB, MigrationKey(20260519_1300)]
public class DefaultDB_20260519_1300_PriceListType : AutoReversingMigration
{
    public override void Up()
    {
        // Add Type column to PriceLists table
        if (!Schema.Table("PriceLists").Column("Type").Exists())
        {
            Alter.Table("PriceLists")
                .AddColumn("Type").AsInt32().WithDefaultValue(1); // Default: Sales = 1
        }
    }
}
