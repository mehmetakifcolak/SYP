using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[DefaultDB, MigrationKey(20260523_1300)]
public class DefaultDB_20260523_1300_OrderDetailLineStatus : AutoReversingMigration
{
    public override void Up()
    {
        Alter.Table("OrderDetails")
            .AddColumn("LineStatus").AsInt32().Nullable()
            .AddColumn("OriginalQuantity").AsDecimal(18, 4).Nullable()
            .AddColumn("ReviseNote").AsString(500).Nullable();
    }
}
