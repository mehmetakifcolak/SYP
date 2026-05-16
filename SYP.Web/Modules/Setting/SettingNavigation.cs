using Serenity.Navigation;
using MyPages = SYP.Setting.Pages;

[assembly: NavigationMenu(5000, "Ayarlar", icon: "fa-cogs")]
[assembly: NavigationLink(5100, "Ayarlar/Günlük Kurlar", typeof(MyPages.DailyExchangesPage), icon: "fa-line-chart")]
[assembly: NavigationLink(5200, "Ayarlar/Numara Şablonları", typeof(MyPages.NumberTemplatesPage), icon: "fa-hashtag")]
[assembly: NavigationLink(5300, "Ayarlar/Banka Hesap Bilgileri", typeof(MyPages.BankAccountInformationsPage), icon: "fa-bank")]
[assembly: NavigationLink(5400, "Ayarlar/Para Birimleri", typeof(MyPages.CurrencyListPage), icon: "fa-money")]
[assembly: NavigationLink(5500, "Ayarlar/KDV Oranları", typeof(MyPages.VatRatesPage), icon: "fa-percent")]
[assembly: NavigationLink(5600, "Ayarlar/Tedarikçi Tipleri", typeof(MyPages.VendorTypePage), icon: "fa-truck")]
[assembly: NavigationLink(5700, "Ayarlar/Ülkeler", typeof(MyPages.CountryPage), icon: "fa-globe")]
