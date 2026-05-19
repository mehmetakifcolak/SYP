using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[DefaultDB, MigrationKey(20260520_1200)]
public class DefaultDB_20260520_1200_ProductsPackingId : AutoReversingMigration
{
    public override void Up()
    {
        if (!Schema.Table("Products").Column("PackingId").Exists())
        {
            Alter.Table("Products")
                .AddColumn("PackingId").AsInt32().Nullable()
                .ForeignKey("FK_Products_ProductPacking", "ProductPacking", "Id");
        }
    }
}
