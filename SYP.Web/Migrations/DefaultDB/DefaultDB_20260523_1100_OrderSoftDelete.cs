using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[DefaultDB, MigrationKey(20260523_1100)]
public class DefaultDB_20260523_1100_OrderSoftDelete : AutoReversingMigration
{
    public override void Up()
    {
        Alter.Table("Orders")
            .AddColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false)
            .AddColumn("DeletedDate").AsDateTime().Nullable()
            .AddColumn("DeletedUserId").AsInt32().Nullable();
    }
}
