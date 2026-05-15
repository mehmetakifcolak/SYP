using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[Migration(20260515_1700)]
public class DefaultDB_20260515_1700_AuditLog : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("AuditLog")
            .WithColumn("Id").AsInt64().Identity().PrimaryKey().NotNullable()
            .WithColumn("EntityType").AsString(100).NotNullable()
            .WithColumn("EntityId").AsString(50).NotNullable()
            .WithColumn("EntityName").AsString(500).Nullable()
            .WithColumn("ActionType").AsString(20).NotNullable() // Insert, Update, Delete
            .WithColumn("ActionDate").AsDateTime().NotNullable()
            .WithColumn("UserId").AsInt32().Nullable()
            .WithColumn("Username").AsString(100).Nullable()
            .WithColumn("IpAddress").AsString(50).Nullable()
            .WithColumn("OldValues").AsString(int.MaxValue).Nullable()
            .WithColumn("NewValues").AsString(int.MaxValue).Nullable()
            .WithColumn("ChangedFields").AsString(1000).Nullable();

        Create.Index("IX_AuditLog_EntityType_EntityId")
            .OnTable("AuditLog")
            .OnColumn("EntityType").Ascending()
            .OnColumn("EntityId").Ascending();

        Create.Index("IX_AuditLog_ActionDate")
            .OnTable("AuditLog")
            .OnColumn("ActionDate").Descending();

        Create.Index("IX_AuditLog_UserId")
            .OnTable("AuditLog")
            .OnColumn("UserId").Ascending();
    }
}
