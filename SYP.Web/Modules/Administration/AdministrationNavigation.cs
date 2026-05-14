using Administration = SYP.Administration.Pages;

[assembly: NavigationMenu(9000, "Yönetim", icon: "fa-cogs")]
[assembly: NavigationLink(9100, "Yönetim/Diller", typeof(Administration.LanguagePage), icon: "fa-comments")]
[assembly: NavigationLink(9200, "Yönetim/Çeviriler", typeof(Administration.TranslationPage), icon: "fa-comment-o")]
[assembly: NavigationLink(9300, "Yönetim/Roller", typeof(Administration.RolePage), icon: "fa-lock")]
[assembly: NavigationLink(9400, "Yönetim/Kullanıcı Yönetimi", typeof(Administration.UserPage), icon: "fa-users")]
