using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[DefaultDB, MigrationKey(20260523_1200)]
public class DefaultDB_20260523_1200_SoftDeleteLogFields : AutoReversingMigration
{
    public override void Up()
    {
        Alter.Table("Customers")
            .AddColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false)
            .AddColumn("DeletedDate").AsDateTime().Nullable()
            .AddColumn("DeletedUserId").AsInt32().Nullable();

        Alter.Table("Products")
            .AddColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false)
            .AddColumn("DeletedDate").AsDateTime().Nullable()
            .AddColumn("DeletedUserId").AsInt32().Nullable();
    }
}
