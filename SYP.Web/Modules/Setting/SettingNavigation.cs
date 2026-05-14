using Serenity.Navigation;
using MyPages = SYP.Setting.Pages;

[assembly: NavigationLink(int.MaxValue, "Setting/Daily Exchanges", typeof(MyPages.DailyExchangesPage), icon: null)]
[assembly: NavigationLink(int.MaxValue, "Setting/Number Templates", typeof(MyPages.NumberTemplatesPage), icon: null)]
[assembly: NavigationLink(int.MaxValue, "Setting/Bank Account Informations", typeof(MyPages.BankAccountInformationsPage), icon: null)]
[assembly: NavigationLink(int.MaxValue, "Setting/Currency List", typeof(MyPages.CurrencyListPage), icon: null)]
[assembly: NavigationLink(int.MaxValue, "Setting/Vat Rates", typeof(MyPages.VatRatesPage), icon: null)]