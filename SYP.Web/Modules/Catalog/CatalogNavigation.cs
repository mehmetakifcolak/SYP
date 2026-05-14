using Serenity.Navigation;
using MyPages = SYP.Catalog.Pages;

[assembly: NavigationMenu(3000, "Katalog", icon: "fa-cubes")]
[assembly: NavigationLink(3100, "Katalog/Ürünler", typeof(MyPages.ProductsPage), icon: "fa-cube")]
