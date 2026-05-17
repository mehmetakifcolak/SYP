using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[Migration(20260517_1800)]
public class DefaultDB_20260517_1800_PriceListsIsActiveToBit : Migration
{
    public override void Up()
    {
        // IsActive kolonunu SMALLINT'ten BIT'e çevir
        if (Schema.Table("PriceLists").Column("IsActive").Exists())
        {
            // Yeni kolon ekle
            Alter.Table("PriceLists").AddColumn("IsActive_Temp").AsBoolean().Nullable();

            // Değerleri kopyala (SMALLINT -> BIT)
            Execute.Sql(@"
                UPDATE PriceLists
                SET IsActive_Temp = CASE
                    WHEN IsActive = 1 THEN 1
                    ELSE 0
                END
            ");

            // Eski kolonu sil
            Delete.Column("IsActive").FromTable("PriceLists");

            // Yeni kolonu yeniden adlandır
            Rename.Column("IsActive_Temp").OnTable("PriceLists").To("IsActive");

            // NOT NULL constraint ekle
            Alter.Table("PriceLists").AlterColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true);
        }
    }

    public override void Down()
    {
        // Geri alma: BIT'ten SMALLINT'e çevir
        if (Schema.Table("PriceLists").Column("IsActive").Exists())
        {
            // Yeni kolon ekle
            Alter.Table("PriceLists").AddColumn("IsActive_Temp").AsInt16().Nullable();

            // Değerleri kopyala (BIT -> SMALLINT)
            Execute.Sql(@"
                UPDATE PriceLists
                SET IsActive_Temp = CASE
                    WHEN IsActive = 1 THEN 1
                    ELSE 0
                END
            ");

            // Eski kolonu sil
            Delete.Column("IsActive").FromTable("PriceLists");

            // Yeni kolonu yeniden adlandır
            Rename.Column("IsActive_Temp").OnTable("PriceLists").To("IsActive");

            // NOT NULL constraint ekle
            Alter.Table("PriceLists").AlterColumn("IsActive").AsInt16().NotNullable().WithDefaultValue(1);
        }
    }
}
