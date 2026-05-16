using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[Migration(20260517_1200)]
public class DefaultDB_20260517_1200_ProductCategoryPaths : Migration
{
    public override void Up()
    {
        // Path ve FullPath sütunlarını kaldır (artık kod tarafında hesaplanıyor)
        Execute.Sql(@"
            IF EXISTS (SELECT 1 FROM sys.columns WHERE name = 'Path' AND object_id = OBJECT_ID('ProductCategory'))
            BEGIN
                ALTER TABLE ProductCategory DROP COLUMN [Path];
            END

            IF EXISTS (SELECT 1 FROM sys.columns WHERE name = 'FullPath' AND object_id = OBJECT_ID('ProductCategory'))
            BEGIN
                ALTER TABLE ProductCategory DROP COLUMN [FullPath];
            END
        ");
    }

    public override void Down()
    {
        // Gerekirse geri eklenebilir
    }
}
