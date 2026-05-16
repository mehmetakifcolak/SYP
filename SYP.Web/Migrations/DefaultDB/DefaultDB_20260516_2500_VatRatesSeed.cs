using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[Migration(20260516_2500)]
public class DefaultDB_20260516_2500_VatRatesSeed : Migration
{
    public override void Up()
    {
        // Eğer VatRates tablosu boşsa varsayılan değerleri ekle
        Execute.Sql(@"
            IF NOT EXISTS (SELECT 1 FROM VatRates)
            BEGIN
                INSERT INTO VatRates (Id, Name, Code, Rate, IsDefault, IsActive, SortOrder) VALUES (1, '% 0', '0', 0, 0, 1, 1);
                INSERT INTO VatRates (Id, Name, Code, Rate, IsDefault, IsActive, SortOrder) VALUES (2, '% 1', '1', 1, 0, 1, 2);
                INSERT INTO VatRates (Id, Name, Code, Rate, IsDefault, IsActive, SortOrder) VALUES (3, '% 10', '10', 10, 0, 1, 3);
                INSERT INTO VatRates (Id, Name, Code, Rate, IsDefault, IsActive, SortOrder) VALUES (4, '% 20', '20', 20, 1, 1, 4);
            END
        ");
    }

    public override void Down()
    {
    }
}
