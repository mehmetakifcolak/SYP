using MyPages = SYP.Order.Pages;
using Serenity.Navigation;

[assembly: NavigationMenu(3000, "Sipariş", icon: "fa-shopping-cart")]
[assembly: NavigationLink(3010, "Sipariş/Siparişler", typeof(MyPages.OrderPage), icon: "fa-list-alt")]
[assembly: NavigationLink(3020, "Sipariş/Kademeli İndirim Ayarları", typeof(MyPages.TieredDiscountSettingsPage), icon: "fa-percent")]
