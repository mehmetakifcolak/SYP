using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[Migration(20260516_2600)]
public class DefaultDB_20260516_2600_Brands : Migration
{
    public override void Up()
    {
        // Brands tablosu zaten Products modülünden var, sadece Products.BrandId ekle
        if (!Schema.Table("Products").Column("BrandId").Exists())
        {
            Alter.Table("Products").AddColumn("BrandId").AsInt32().Nullable();
        }
    }

    public override void Down()
    {
        if (Schema.Table("Products").Column("BrandId").Exists())
            Delete.Column("BrandId").FromTable("Products");
    }
}
