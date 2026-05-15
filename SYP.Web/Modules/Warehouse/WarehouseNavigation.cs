using Serenity.Navigation;
using MyPages = SYP.Warehouse.Pages;

[assembly: NavigationMenu(7000, "Depo", icon: "fa-warehouse")]
[assembly: NavigationLink(7100, "Depo/Depolar", typeof(MyPages.WarehousesPage), icon: "fa-building")]
[assembly: NavigationLink(7200, "Depo/Stok Girişleri", typeof(MyPages.StockEntriesPage), icon: "fa-arrow-down")]
[assembly: NavigationLink(7300, "Depo/Stok Durumu", typeof(MyPages.WarehouseStockPage), icon: "fa-boxes")]
