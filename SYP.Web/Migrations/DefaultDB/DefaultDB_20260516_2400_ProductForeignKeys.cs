using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[Migration(20260516_2400)]
public class DefaultDB_20260516_2400_ProductForeignKeys : Migration
{
    public override void Up()
    {
        // Eski string/decimal alanları sil (varsa)
        if (Schema.Table("Products").Column("Unit").Exists())
            Delete.Column("Unit").FromTable("Products");

        if (Schema.Table("Products").Column("Currency").Exists())
            Delete.Column("Currency").FromTable("Products");

        if (Schema.Table("Products").Column("VatRate").Exists())
            Delete.Column("VatRate").FromTable("Products");

        // Yeni foreign key alanları ekle
        if (!Schema.Table("Products").Column("UnitId").Exists())
            Alter.Table("Products").AddColumn("UnitId").AsInt32().Nullable();

        if (!Schema.Table("Products").Column("CurrencyId").Exists())
            Alter.Table("Products").AddColumn("CurrencyId").AsInt32().Nullable();

        if (!Schema.Table("Products").Column("VatRateId").Exists())
            Alter.Table("Products").AddColumn("VatRateId").AsInt32().Nullable();
    }

    public override void Down()
    {
        if (Schema.Table("Products").Column("VatRateId").Exists())
            Delete.Column("VatRateId").FromTable("Products");

        if (Schema.Table("Products").Column("CurrencyId").Exists())
            Delete.Column("CurrencyId").FromTable("Products");

        if (Schema.Table("Products").Column("UnitId").Exists())
            Delete.Column("UnitId").FromTable("Products");
    }
}
