using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[Migration(20260515_1200)]
public class DefaultDB_20260515_1200_CustomerUserId : AutoReversingMigration
{
    public override void Up()
    {
        Alter.Table("Customers")
            .AddColumn("UserId").AsInt32().Nullable()
            .ForeignKey("FK_Customers_Users", "Users", "UserId");
    }
}
