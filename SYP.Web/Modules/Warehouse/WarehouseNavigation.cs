using Serenity.Navigation;
using MyPages = SYP.Warehouse.Pages;

[assembly: NavigationMenu(4000, "Warehouse", icon: "fa-warehouse")]
[assembly: NavigationLink(4100, "Warehouse/Warehouses", typeof(MyPages.WarehousesPage), icon: "fa-building")]
[assembly: NavigationLink(4200, "Warehouse/Stock Entries", typeof(MyPages.StockEntriesPage), icon: "fa-arrow-down")]
[assembly: NavigationLink(4250, "Warehouse/Stock Exits", typeof(MyPages.StockExitsPage), icon: "fa-arrow-up")]
[assembly: NavigationLink(4280, "Warehouse/Stock Movements", typeof(MyPages.StockMovementsPage), icon: "fa-exchange-alt")]
[assembly: NavigationLink(4300, "Warehouse/Stock Status", typeof(MyPages.WarehouseStockPage), icon: "fa-boxes")]
