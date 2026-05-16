using Serenity.Navigation;
using MyPages = SYP.Catalog.Pages;
using ProductPages = SYP.Products.Pages;

[assembly: NavigationMenu(3000, "Katalog", icon: "fa-cubes")]
[assembly: NavigationLink(3100, "Katalog/Ürünler", typeof(MyPages.ProductsPage), icon: "fa-cube")]
[assembly: NavigationLink(3200, "Katalog/Markalar", typeof(ProductPages.BrandsPage), icon: "fa-tags")]
[assembly: NavigationLink(3300, "Katalog/Kategoriler", typeof(ProductPages.ProductCategoryPage), icon: "fa-sitemap")]
[assembly: NavigationLink(3400, "Katalog/Fiyat Listeleri", typeof(MyPages.PriceListsPage), icon: "fa-list-alt")]
