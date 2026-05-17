using Serenity.Navigation;
using MyPages = SYP.Catalog.Pages;

[assembly: NavigationMenu(3000, "Katalog", icon: "fa-cubes")]
[assembly: NavigationLink(3100, "Katalog/Ürünler", typeof(MyPages.ProductsPage), icon: "fa-cube")]
[assembly: NavigationLink(3200, "Katalog/Markalar", typeof(MyPages.BrandsPage), icon: "fa-tags")]
[assembly: NavigationLink(3300, "Katalog/Kategoriler", typeof(MyPages.ProductCategoryPage), icon: "fa-sitemap")]
[assembly: NavigationLink(int.MaxValue, "Katalog/Price Lists", typeof(MyPages.PriceListsPage), icon: "fa-list-alt")]
