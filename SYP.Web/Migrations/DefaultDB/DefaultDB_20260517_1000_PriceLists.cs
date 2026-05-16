using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[Migration(20260517_1000)]
public class DefaultDB_20260517_1000_PriceLists : Migration
{
    public override void Up()
    {
        Create.Table("PriceLists")
            .WithColumn("Id").AsInt32().Identity().PrimaryKey()
            .WithColumn("Code").AsString(50).NotNullable()
            .WithColumn("Name").AsString(200).NotNullable()
            .WithColumn("Description").AsString(500).Nullable()
            .WithColumn("CurrencyId").AsInt32().NotNullable()
            .WithColumn("ValidFrom").AsDateTime().Nullable()
            .WithColumn("ValidTo").AsDateTime().Nullable()
            .WithColumn("IsActive").AsInt16().NotNullable().WithDefaultValue(1)
            .WithColumn("IsDefault").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("InsertDate").AsDateTime().Nullable()
            .WithColumn("InsertUserId").AsInt32().Nullable()
            .WithColumn("UpdateDate").AsDateTime().Nullable()
            .WithColumn("UpdateUserId").AsInt32().Nullable();

        Create.Table("PriceListItems")
            .WithColumn("Id").AsInt32().Identity().PrimaryKey()
            .WithColumn("PriceListId").AsInt32().NotNullable()
            .WithColumn("ProductId").AsInt32().NotNullable()
            .WithColumn("UnitPrice").AsDecimal(18, 4).NotNullable()
            .WithColumn("DiscountRate").AsDecimal(5, 2).Nullable()
            .WithColumn("Notes").AsString(500).Nullable();

        Create.ForeignKey("FK_PriceListItems_PriceListId")
            .FromTable("PriceListItems").ForeignColumn("PriceListId")
            .ToTable("PriceLists").PrimaryColumn("Id");

        Create.ForeignKey("FK_PriceListItems_ProductId")
            .FromTable("PriceListItems").ForeignColumn("ProductId")
            .ToTable("Products").PrimaryColumn("Id");

        Create.ForeignKey("FK_PriceLists_CurrencyId")
            .FromTable("PriceLists").ForeignColumn("CurrencyId")
            .ToTable("CurrencyList").PrimaryColumn("Id");

        Create.Index("IX_PriceListItems_PriceList_Product")
            .OnTable("PriceListItems")
            .OnColumn("PriceListId").Ascending()
            .OnColumn("ProductId").Ascending()
            .WithOptions().Unique();
    }

    public override void Down()
    {
        Delete.ForeignKey("FK_PriceListItems_PriceListId").OnTable("PriceListItems");
        Delete.ForeignKey("FK_PriceListItems_ProductId").OnTable("PriceListItems");
        Delete.ForeignKey("FK_PriceLists_CurrencyId").OnTable("PriceLists");
        Delete.Index("IX_PriceListItems_PriceList_Product").OnTable("PriceListItems");
        Delete.Table("PriceListItems");
        Delete.Table("PriceLists");
    }
}
