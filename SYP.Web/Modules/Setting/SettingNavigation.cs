using Serenity.Navigation;
using MyPages = SYP.Setting.Pages;

[assembly: NavigationMenu(5000, "Settings", icon: "fa-cogs")]
[assembly: NavigationLink(5100, "Settings/Daily Exchange Rates", typeof(MyPages.DailyExchangesPage), icon: "fa-line-chart")]
[assembly: NavigationLink(5200, "Settings/Number Templates", typeof(MyPages.NumberTemplatesPage), icon: "fa-hashtag")]
[assembly: NavigationLink(5300, "Settings/Bank Account Information", typeof(MyPages.BankAccountInformationsPage), icon: "fa-bank")]
[assembly: NavigationLink(5400, "Settings/Currencies", typeof(MyPages.CurrencyListPage), icon: "fa-money")]
[assembly: NavigationLink(5500, "Settings/VAT Rates", typeof(MyPages.VatRatesPage), icon: "fa-percent")]
[assembly: NavigationLink(5600, "Settings/Vendor Types", typeof(MyPages.VendorTypePage), icon: "fa-truck")]
[assembly: NavigationLink(5700, "Settings/Countries", typeof(MyPages.CountryPage), icon: "fa-globe")]
