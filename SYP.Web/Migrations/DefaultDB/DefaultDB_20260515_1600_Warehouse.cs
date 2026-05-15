using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[Migration(20260515_1600)]
public class DefaultDB_20260515_1600_Warehouse : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("Warehouses")
            .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
            .WithColumn("Code").AsString(50).NotNullable()
            .WithColumn("Name").AsString(200).NotNullable()
            .WithColumn("Address").AsString(500).Nullable()
            .WithColumn("City").AsString(100).Nullable()
            .WithColumn("Phone").AsString(50).Nullable()
            .WithColumn("Email").AsString(200).Nullable()
            .WithColumn("ManagerName").AsString(200).Nullable()
            .WithColumn("Description").AsString(500).Nullable()
            .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("InsertDate").AsDateTime().Nullable()
            .WithColumn("InsertUserId").AsInt32().Nullable()
            .WithColumn("UpdateDate").AsDateTime().Nullable()
            .WithColumn("UpdateUserId").AsInt32().Nullable();

        Create.UniqueConstraint("UQ_Warehouses_Code")
            .OnTable("Warehouses")
            .Column("Code");
    }
}
