using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[DefaultDB, MigrationKey(20260523_1000)]
public class DefaultDB_20260523_1000_OrderIsStockExitCreated : AutoReversingMigration
{
    public override void Up()
    {
        if (!Schema.Table("Orders").Column("IsStockExitCreated").Exists())
        {
            Alter.Table("Orders")
                .AddColumn("IsStockExitCreated").AsBoolean().NotNullable().WithDefaultValue(false);
        }
    }
}
