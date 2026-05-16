using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[Migration(20260517_1100)]
public class DefaultDB_20260517_1100_ProductCategoryId : Migration
{
    public override void Up()
    {
        // Products tablosuna CategoryId alanı ekle
        if (!Schema.Table("Products").Column("CategoryId").Exists())
        {
            Alter.Table("Products")
                .AddColumn("CategoryId").AsInt32().Nullable()
                .ForeignKey("FK_Products_ProductCategory", "ProductCategory", "Id");
        }
    }

    public override void Down()
    {
        // Down migration - CategoryId alanını sil
        if (Schema.Table("Products").Column("CategoryId").Exists())
            Delete.Column("CategoryId").FromTable("Products");
    }
}
