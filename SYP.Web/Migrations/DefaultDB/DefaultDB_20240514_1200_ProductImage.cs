using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[Migration(20240514_1200)]
public class DefaultDB_20240514_1200_ProductImage : AutoReversingMigration
{
    public override void Up()
    {
        Alter.Table("Products")
            .AddColumn("ProductImage").AsString(200).Nullable();
    }
}
