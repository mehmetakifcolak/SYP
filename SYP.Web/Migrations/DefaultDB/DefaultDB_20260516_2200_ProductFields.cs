using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[Migration(20260516_2200)]
public class DefaultDB_20260516_2200_ProductFields : Migration
{
    public override void Up()
    {
        // Products tablosuna sütun ekle (varsa atla)
        if (!Schema.Table("Products").Column("Unit").Exists())
            Alter.Table("Products").AddColumn("Unit").AsString(10).Nullable();

        if (!Schema.Table("Products").Column("Currency").Exists())
            Alter.Table("Products").AddColumn("Currency").AsString(10).Nullable();

        if (!Schema.Table("Products").Column("VatRate").Exists())
            Alter.Table("Products").AddColumn("VatRate").AsDecimal(18, 4).Nullable();

        if (!Schema.Table("Products").Column("UnitPrice").Exists())
            Alter.Table("Products").AddColumn("UnitPrice").AsDecimal(18, 4).Nullable();

        // StockEntryDetails tablosuna sütun ekle (varsa atla)
        if (!Schema.Table("StockEntryDetails").Column("Unit").Exists())
            Alter.Table("StockEntryDetails").AddColumn("Unit").AsString(10).Nullable();

        if (!Schema.Table("StockEntryDetails").Column("Currency").Exists())
            Alter.Table("StockEntryDetails").AddColumn("Currency").AsString(10).Nullable();

        if (!Schema.Table("StockEntryDetails").Column("VatRate").Exists())
            Alter.Table("StockEntryDetails").AddColumn("VatRate").AsDecimal(18, 4).Nullable();

        if (!Schema.Table("StockEntryDetails").Column("UnitPrice").Exists())
            Alter.Table("StockEntryDetails").AddColumn("UnitPrice").AsDecimal(18, 4).Nullable();

        // StockExitDetails tablosuna sütun ekle (varsa atla)
        if (!Schema.Table("StockExitDetails").Column("Unit").Exists())
            Alter.Table("StockExitDetails").AddColumn("Unit").AsString(10).Nullable();

        if (!Schema.Table("StockExitDetails").Column("Currency").Exists())
            Alter.Table("StockExitDetails").AddColumn("Currency").AsString(10).Nullable();

        if (!Schema.Table("StockExitDetails").Column("VatRate").Exists())
            Alter.Table("StockExitDetails").AddColumn("VatRate").AsDecimal(18, 4).Nullable();

        if (!Schema.Table("StockExitDetails").Column("UnitPrice").Exists())
            Alter.Table("StockExitDetails").AddColumn("UnitPrice").AsDecimal(18, 4).Nullable();
    }

    public override void Down()
    {
        // Down migration - sütunları sil
        if (Schema.Table("Products").Column("UnitPrice").Exists())
            Delete.Column("UnitPrice").FromTable("Products");

        if (Schema.Table("Products").Column("VatRate").Exists())
            Delete.Column("VatRate").FromTable("Products");

        if (Schema.Table("Products").Column("Currency").Exists())
            Delete.Column("Currency").FromTable("Products");

        if (Schema.Table("Products").Column("Unit").Exists())
            Delete.Column("Unit").FromTable("Products");

        if (Schema.Table("StockEntryDetails").Column("UnitPrice").Exists())
            Delete.Column("UnitPrice").FromTable("StockEntryDetails");

        if (Schema.Table("StockEntryDetails").Column("VatRate").Exists())
            Delete.Column("VatRate").FromTable("StockEntryDetails");

        if (Schema.Table("StockEntryDetails").Column("Currency").Exists())
            Delete.Column("Currency").FromTable("StockEntryDetails");

        if (Schema.Table("StockEntryDetails").Column("Unit").Exists())
            Delete.Column("Unit").FromTable("StockEntryDetails");

        if (Schema.Table("StockExitDetails").Column("UnitPrice").Exists())
            Delete.Column("UnitPrice").FromTable("StockExitDetails");

        if (Schema.Table("StockExitDetails").Column("VatRate").Exists())
            Delete.Column("VatRate").FromTable("StockExitDetails");

        if (Schema.Table("StockExitDetails").Column("Currency").Exists())
            Delete.Column("Currency").FromTable("StockExitDetails");

        if (Schema.Table("StockExitDetails").Column("Unit").Exists())
            Delete.Column("Unit").FromTable("StockExitDetails");
    }
}
