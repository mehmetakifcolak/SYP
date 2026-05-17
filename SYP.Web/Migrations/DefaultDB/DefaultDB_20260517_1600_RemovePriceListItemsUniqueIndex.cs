using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[Migration(20260517_1600)]
public class DefaultDB_20260517_1600_RemovePriceListItemsUniqueIndex : Migration
{
    public override void Up()
    {
        // Unique index'i sil - aynı ürün farklı fiyatlarla eklenebilsin
        Delete.Index("IX_PriceListItems_PriceList_Product").OnTable("PriceListItems");

        // Non-unique index ekle - performans için
        Create.Index("IX_PriceListItems_PriceList_Product")
            .OnTable("PriceListItems")
            .OnColumn("PriceListId").Ascending()
            .OnColumn("ProductId").Ascending();
    }

    public override void Down()
    {
        // Geri alma: non-unique index'i sil
        Delete.Index("IX_PriceListItems_PriceList_Product").OnTable("PriceListItems");

        // Unique index'i geri koy
        Create.Index("IX_PriceListItems_PriceList_Product")
            .OnTable("PriceListItems")
            .OnColumn("PriceListId").Ascending()
            .OnColumn("ProductId").Ascending()
            .WithOptions().Unique();
    }
}
