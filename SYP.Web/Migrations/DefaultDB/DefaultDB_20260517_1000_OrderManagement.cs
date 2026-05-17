using FluentMigrator;
using System;

namespace SYP.Migrations.DefaultDB;

[DefaultDB, MigrationKey(20260517_1000)]
public class DefaultDB_20260517_1000_OrderManagement : AutoReversingMigration
{
    public override void Up()
    {
        // Orders Table
        Create.Table("Orders")
            .WithColumn("Id").AsInt32().IdentityKey(this)
            .WithColumn("OrderNumber").AsString(50).NotNullable().Unique("IX_Orders_OrderNumber")
            .WithColumn("CustomerId").AsInt32().NotNullable()
            .WithColumn("ManagerUserId").AsInt32().Nullable()
            .WithColumn("Status").AsInt32().NotNullable().WithDefaultValue(1)
            .WithColumn("OrderDate").AsDateTime().NotNullable()
            .WithColumn("TotalAmount").AsDecimal(18, 4).NotNullable().WithDefaultValue(0)
            .WithColumn("DiscountPercentage").AsDecimal(5, 2).Nullable()
            .WithColumn("DiscountAmount").AsDecimal(18, 4).NotNullable().WithDefaultValue(0)
            .WithColumn("NetAmount").AsDecimal(18, 4).NotNullable().WithDefaultValue(0)
            .WithColumn("CurrencyId").AsInt32().Nullable()
            .WithColumn("Notes").AsString(int.MaxValue).Nullable()
            .WithColumn("RejectReason").AsString(int.MaxValue).Nullable()
            .WithColumn("InsertDate").AsDateTime().NotNullable()
            .WithColumn("InsertUserId").AsInt32().NotNullable()
            .WithColumn("UpdateDate").AsDateTime().Nullable()
            .WithColumn("UpdateUserId").AsInt32().Nullable();

        Create.ForeignKey("FK_Orders_Customers")
            .FromTable("Orders").ForeignColumn("CustomerId")
            .ToTable("Customers").PrimaryColumn("Id");

        Create.ForeignKey("FK_Orders_ManagerUser")
            .FromTable("Orders").ForeignColumn("ManagerUserId")
            .ToTable("Users").PrimaryColumn("UserId");

        Create.ForeignKey("FK_Orders_Currency")
            .FromTable("Orders").ForeignColumn("CurrencyId")
            .ToTable("CurrencyList").PrimaryColumn("Id");

        Create.Index("IX_Orders_CustomerId")
            .OnTable("Orders").OnColumn("CustomerId");

        Create.Index("IX_Orders_ManagerUserId")
            .OnTable("Orders").OnColumn("ManagerUserId");

        Create.Index("IX_Orders_Status")
            .OnTable("Orders").OnColumn("Status");

        // OrderDetails Table
        Create.Table("OrderDetails")
            .WithColumn("Id").AsInt32().IdentityKey(this)
            .WithColumn("OrderId").AsInt32().NotNullable()
            .WithColumn("ProductId").AsInt32().NotNullable()
            .WithColumn("Quantity").AsDecimal(18, 4).NotNullable()
            .WithColumn("UnitId").AsInt32().Nullable()
            .WithColumn("UnitPrice").AsDecimal(18, 4).NotNullable()
            .WithColumn("VatRateId").AsInt32().Nullable()
            .WithColumn("VatRate").AsDecimal(5, 2).NotNullable().WithDefaultValue(0)
            .WithColumn("Discount").AsDecimal(18, 4).NotNullable().WithDefaultValue(0)
            .WithColumn("LineTotal").AsDecimal(18, 4).NotNullable()
            .WithColumn("Notes").AsString(500).Nullable();

        Create.ForeignKey("FK_OrderDetails_Orders")
            .FromTable("OrderDetails").ForeignColumn("OrderId")
            .ToTable("Orders").PrimaryColumn("Id")
            .OnDelete(System.Data.Rule.Cascade);

        Create.ForeignKey("FK_OrderDetails_Products")
            .FromTable("OrderDetails").ForeignColumn("ProductId")
            .ToTable("Products").PrimaryColumn("Id");

        Create.ForeignKey("FK_OrderDetails_Units")
            .FromTable("OrderDetails").ForeignColumn("UnitId")
            .ToTable("Units").PrimaryColumn("Id");

        Create.ForeignKey("FK_OrderDetails_VatRates")
            .FromTable("OrderDetails").ForeignColumn("VatRateId")
            .ToTable("VatRates").PrimaryColumn("Id");

        Create.Index("IX_OrderDetails_OrderId")
            .OnTable("OrderDetails").OnColumn("OrderId");

        // OrderStatusHistory Table
        Create.Table("OrderStatusHistory")
            .WithColumn("Id").AsInt64().IdentityKey(this)
            .WithColumn("OrderId").AsInt32().NotNullable()
            .WithColumn("OldStatus").AsInt32().Nullable()
            .WithColumn("NewStatus").AsInt32().NotNullable()
            .WithColumn("ChangedByUserId").AsInt32().NotNullable()
            .WithColumn("ChangedByUserRole").AsString(50).Nullable()
            .WithColumn("ChangeReason").AsString(int.MaxValue).Nullable()
            .WithColumn("ChangeDate").AsDateTime().NotNullable();

        Create.ForeignKey("FK_OrderStatusHistory_Orders")
            .FromTable("OrderStatusHistory").ForeignColumn("OrderId")
            .ToTable("Orders").PrimaryColumn("Id")
            .OnDelete(System.Data.Rule.Cascade);

        Create.ForeignKey("FK_OrderStatusHistory_Users")
            .FromTable("OrderStatusHistory").ForeignColumn("ChangedByUserId")
            .ToTable("Users").PrimaryColumn("UserId");

        Create.Index("IX_OrderStatusHistory_OrderId")
            .OnTable("OrderStatusHistory").OnColumn("OrderId");

        // OrderDocuments Table
        Create.Table("OrderDocuments")
            .WithColumn("Id").AsInt32().IdentityKey(this)
            .WithColumn("OrderId").AsInt32().NotNullable()
            .WithColumn("DocumentType").AsInt32().NotNullable().WithDefaultValue(1)
            .WithColumn("FileName").AsString(255).NotNullable()
            .WithColumn("FilePath").AsString(500).NotNullable()
            .WithColumn("FileSize").AsInt64().NotNullable()
            .WithColumn("MimeType").AsString(100).Nullable()
            .WithColumn("UploadedByUserId").AsInt32().NotNullable()
            .WithColumn("UploadDate").AsDateTime().NotNullable()
            .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("Notes").AsString(500).Nullable();

        Create.ForeignKey("FK_OrderDocuments_Orders")
            .FromTable("OrderDocuments").ForeignColumn("OrderId")
            .ToTable("Orders").PrimaryColumn("Id")
            .OnDelete(System.Data.Rule.Cascade);

        Create.ForeignKey("FK_OrderDocuments_Users")
            .FromTable("OrderDocuments").ForeignColumn("UploadedByUserId")
            .ToTable("Users").PrimaryColumn("UserId");

        // TieredDiscountSettings Table
        Create.Table("TieredDiscountSettings")
            .WithColumn("Id").AsInt32().IdentityKey(this)
            .WithColumn("MinAmount").AsDecimal(18, 4).NotNullable()
            .WithColumn("DiscountPercentage").AsDecimal(5, 2).NotNullable()
            .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("DisplayOrder").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("InsertDate").AsDateTime().NotNullable()
            .WithColumn("InsertUserId").AsInt32().NotNullable()
            .WithColumn("UpdateDate").AsDateTime().Nullable()
            .WithColumn("UpdateUserId").AsInt32().Nullable();

        // Default TieredDiscountSettings
        Insert.IntoTable("TieredDiscountSettings").Row(new
        {
            MinAmount = 15000,
            DiscountPercentage = 10,
            IsActive = true,
            DisplayOrder = 1,
            InsertDate = DateTime.Now,
            InsertUserId = 1
        });

        Insert.IntoTable("TieredDiscountSettings").Row(new
        {
            MinAmount = 50000,
            DiscountPercentage = 15,
            IsActive = true,
            DisplayOrder = 2,
            InsertDate = DateTime.Now,
            InsertUserId = 1
        });

        // Add ManagerUserId to Customers Table
        if (!Schema.Table("Customers").Column("ManagerUserId").Exists())
        {
            Alter.Table("Customers")
                .AddColumn("ManagerUserId").AsInt32().Nullable();

            Create.ForeignKey("FK_Customers_ManagerUser")
                .FromTable("Customers").ForeignColumn("ManagerUserId")
                .ToTable("Users").PrimaryColumn("UserId");

            Create.Index("IX_Customers_ManagerUserId")
                .OnTable("Customers").OnColumn("ManagerUserId");
        }
    }
}
