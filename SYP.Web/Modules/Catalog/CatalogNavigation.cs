using Serenity.Navigation;
using MyPages = SYP.Catalog.Pages;

[assembly: NavigationMenu(3000, "Catalog", icon: "fa-cubes")]
[assembly: NavigationLink(3100, "Catalog/Products", typeof(MyPages.ProductsPage), icon: "fa-cube")]
[assembly: NavigationLink(3200, "Catalog/Brands", typeof(MyPages.BrandsPage), icon: "fa-tags")]
[assembly: NavigationLink(3300, "Catalog/Categories", typeof(MyPages.ProductCategoryPage), icon: "fa-sitemap")]
[assembly: NavigationLink(3400, "Catalog/Price Lists", typeof(MyPages.PriceListsPage), icon: "fa-list-alt")]
