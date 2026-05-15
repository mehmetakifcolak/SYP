using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[Migration(20260515_1300)]
public class DefaultDB_20260515_1300_CustomerVendorType : AutoReversingMigration
{
    public override void Up()
    {
        Alter.Table("Customers")
            .AddColumn("VendorTypeId").AsInt32().Nullable()
            .ForeignKey("FK_Customers_VendorType", "VendorType", "Id");
    }
}
