using Serenity.Navigation;
using MyPages = SYP.Customer.Pages;

[assembly: NavigationMenu(2000, "Müşteriler", icon: "fa-users")]
[assembly: NavigationLink(2100, "Müşteriler/Müşteri Listesi", typeof(MyPages.CustomersPage), icon: "fa-user")]
