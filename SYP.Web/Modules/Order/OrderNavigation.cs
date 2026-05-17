using MyPages = SYP.Order.Pages;
using Serenity.Navigation;


[assembly: NavigationLink(int.MaxValue, "Order/Order Detail", typeof(MyPages.OrderDetailPage), icon: null)]
[assembly: NavigationLink(int.MaxValue, "Order/Order Document", typeof(MyPages.OrderDocumentPage), icon: null)]
[assembly: NavigationLink(int.MaxValue, "Order/Order", typeof(MyPages.OrderPage), icon: null)]
[assembly: NavigationLink(int.MaxValue, "Order/Order Status Hist", typeof(MyPages.OrderStatusHistPage), icon: null)]
[assembly: NavigationLink(int.MaxValue, "Order/Tiered Discount Settings", typeof(MyPages.TieredDiscountSettingsPage), icon: null)]
