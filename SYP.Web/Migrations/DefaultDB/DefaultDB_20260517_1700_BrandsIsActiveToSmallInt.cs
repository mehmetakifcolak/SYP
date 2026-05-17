using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[Migration(20260517_1700)]
public class DefaultDB_20260517_1700_BrandsIsActiveToSmallInt : Migration
{
    public override void Up()
    {
        // IsActive kolonunu BIT'ten SMALLINT'e çevir
        // Önce mevcut değerleri koru (BIT: 0=false, 1=true -> SMALLINT: 0, 1)
        if (Schema.Table("Brands").Column("IsActive").Exists())
        {
            // Yeni kolon ekle
            Alter.Table("Brands").AddColumn("IsActive_Temp").AsInt16().Nullable();

            // Değerleri kopyala (BIT -> SMALLINT)
            Execute.Sql(@"
                UPDATE Brands
                SET IsActive_Temp = CASE
                    WHEN IsActive = 1 THEN 1
                    ELSE 0
                END
            ");

            // Eski kolonu sil
            Delete.Column("IsActive").FromTable("Brands");

            // Yeni kolonu yeniden adlandır
            Rename.Column("IsActive_Temp").OnTable("Brands").To("IsActive");
        }
    }

    public override void Down()
    {
        // Geri alma: SMALLINT'ten BIT'e çevir
        if (Schema.Table("Brands").Column("IsActive").Exists())
        {
            // Yeni kolon ekle
            Alter.Table("Brands").AddColumn("IsActive_Temp").AsBoolean().Nullable();

            // Değerleri kopyala (SMALLINT -> BIT)
            Execute.Sql(@"
                UPDATE Brands
                SET IsActive_Temp = CASE
                    WHEN IsActive = 1 THEN 1
                    ELSE 0
                END
            ");

            // Eski kolonu sil
            Delete.Column("IsActive").FromTable("Brands");

            // Yeni kolonu yeniden adlandır
            Rename.Column("IsActive_Temp").OnTable("Brands").To("IsActive");
        }
    }
}
