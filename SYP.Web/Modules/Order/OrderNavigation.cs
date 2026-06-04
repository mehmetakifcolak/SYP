using MyPages = SYP.Order.Pages;
using Serenity.Navigation;

[assembly: NavigationLink(2200, "Siparişler", typeof(MyPages.OrderPage), icon: "fa-list-alt")]
[assembly: NavigationLink(3020, "Kademeli İndirim Ayarları", typeof(MyPages.TieredDiscountSettingsPage), icon: "fa-percent")]
