using Serenity.Navigation;
using MyPages = SYP.Warehouse.Pages;

[assembly: NavigationMenu(4000, "Depo", icon: "fa-warehouse")]
[assembly: NavigationLink(4100, "Depo/Depolar", typeof(MyPages.WarehousesPage), icon: "fa-building")]
[assembly: NavigationLink(4200, "Depo/Stok Girişleri", typeof(MyPages.StockEntriesPage), icon: "fa-arrow-down")]
[assembly: NavigationLink(4250, "Depo/Stok Çıkışları", typeof(MyPages.StockExitsPage), icon: "fa-arrow-up")]
[assembly: NavigationLink(4280, "Depo/Stok Hareketleri", typeof(MyPages.StockMovementsPage), icon: "fa-exchange-alt")]
[assembly: NavigationLink(4300, "Depo/Stok Durumu", typeof(MyPages.WarehouseStockPage), icon: "fa-boxes")]
