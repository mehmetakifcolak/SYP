using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[DefaultDB, MigrationKey(20260519_1200)]
public class DefaultDB_20260519_1200_CustomerCurrency : AutoReversingMigration
{
    public override void Up()
    {
        // Add CurrencyId column to Customers table
        if (!Schema.Table("Customers").Column("CurrencyId").Exists())
        {
            Alter.Table("Customers")
                .AddColumn("CurrencyId").AsInt32().Nullable();
        }

        if (!Schema.Table("Customers").Constraint("FK_Customers_Currency").Exists())
        {
            Create.ForeignKey("FK_Customers_Currency")
                .FromTable("Customers").ForeignColumn("CurrencyId")
                .ToTable("CurrencyList").PrimaryColumn("Id");
        }

        if (!Schema.Table("Customers").Index("IX_Customers_CurrencyId").Exists())
        {
            Create.Index("IX_Customers_CurrencyId")
                .OnTable("Customers").OnColumn("CurrencyId");
        }
    }
}
