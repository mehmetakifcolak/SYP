using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[Migration(20260516_2300)]
public class DefaultDB_20260516_2300_Units : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("Units")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Code").AsString(10).NotNullable()
            .WithColumn("Name").AsString(50).NotNullable()
            .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("SortOrder").AsInt32().NotNullable().WithDefaultValue(0);

        // Varsayılan birimler
        Insert.IntoTable("Units").Row(new { Code = "ADET", Name = "Adet", IsActive = true, SortOrder = 1 });
        Insert.IntoTable("Units").Row(new { Code = "KG", Name = "Kilogram", IsActive = true, SortOrder = 2 });
        Insert.IntoTable("Units").Row(new { Code = "GR", Name = "Gram", IsActive = true, SortOrder = 3 });
        Insert.IntoTable("Units").Row(new { Code = "LT", Name = "Litre", IsActive = true, SortOrder = 4 });
        Insert.IntoTable("Units").Row(new { Code = "ML", Name = "Mililitre", IsActive = true, SortOrder = 5 });
        Insert.IntoTable("Units").Row(new { Code = "MT", Name = "Metre", IsActive = true, SortOrder = 6 });
        Insert.IntoTable("Units").Row(new { Code = "CM", Name = "Santimetre", IsActive = true, SortOrder = 7 });
        Insert.IntoTable("Units").Row(new { Code = "M2", Name = "Metrekare", IsActive = true, SortOrder = 8 });
        Insert.IntoTable("Units").Row(new { Code = "PAKET", Name = "Paket", IsActive = true, SortOrder = 9 });
        Insert.IntoTable("Units").Row(new { Code = "KUTU", Name = "Kutu", IsActive = true, SortOrder = 10 });
        Insert.IntoTable("Units").Row(new { Code = "KOLI", Name = "Koli", IsActive = true, SortOrder = 11 });

        // VatRates tablosuna Rate alanı ekle
        if (!Schema.Table("VatRates").Column("Rate").Exists())
            Alter.Table("VatRates").AddColumn("Rate").AsDecimal(18, 4).Nullable();
    }
}
