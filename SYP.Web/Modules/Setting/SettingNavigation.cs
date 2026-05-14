using Serenity.Navigation;
using MyPages = SYP.Setting.Pages;

[assembly: NavigationMenu(8000, "Ayarlar", icon: "fa-cogs")]
[assembly: NavigationLink(8100, "Ayarlar/Günlük Kurlar", typeof(MyPages.DailyExchangesPage), icon: "fa-line-chart")]
[assembly: NavigationLink(8200, "Ayarlar/Numara Şablonları", typeof(MyPages.NumberTemplatesPage), icon: "fa-hashtag")]
[assembly: NavigationLink(8300, "Ayarlar/Banka Hesap Bilgileri", typeof(MyPages.BankAccountInformationsPage), icon: "fa-bank")]
[assembly: NavigationLink(8400, "Ayarlar/Para Birimleri", typeof(MyPages.CurrencyListPage), icon: "fa-money")]
[assembly: NavigationLink(8500, "Ayarlar/KDV Oranları", typeof(MyPages.VatRatesPage), icon: "fa-percent")]
