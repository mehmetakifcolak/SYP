using Serenity.Navigation;
using MyPages = SYP.Customer.Pages;

[assembly: NavigationMenu(2000, "Customers", icon: "fa-users")]
[assembly: NavigationLink(2100, "Customers/Customer List", typeof(MyPages.CustomersPage), icon: "fa-user")]
