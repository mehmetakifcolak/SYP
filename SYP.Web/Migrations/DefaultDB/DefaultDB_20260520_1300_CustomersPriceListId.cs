using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[DefaultDB, MigrationKey(20260520_1300)]
public class DefaultDB_20260520_1300_CustomersPriceListId : AutoReversingMigration
{
    public override void Up()
    {
        if (!Schema.Table("Customers").Column("PriceListId").Exists())
        {
            Alter.Table("Customers")
                .AddColumn("PriceListId").AsInt32().Nullable()
                .ForeignKey("FK_Customers_PriceLists", "PriceLists", "Id");
        }
    }
}
