using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[DefaultDB, MigrationKey(20260520_1100)]
public class DefaultDB_20260520_1100_ProductPacking : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("ProductPacking")
            .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("Code").AsString(50).NotNullable()
            .WithColumn("Name").AsString(200).NotNullable()
            .WithColumn("Description").AsString(int.MaxValue).Nullable()
            .WithColumn("Quantity").AsInt32().NotNullable().WithDefaultValue(1)
            .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("InsertDate").AsDateTime().Nullable()
            .WithColumn("InsertUserId").AsInt32().Nullable()
            .WithColumn("UpdateDate").AsDateTime().Nullable()
            .WithColumn("UpdateUserId").AsInt32().Nullable();
    }
}
