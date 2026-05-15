using Administration = SYP.Administration.Pages;

[assembly: NavigationMenu(10000, "Yönetim", icon: "fa-cogs")]
[assembly: NavigationLink(10100, "Yönetim/Diller", typeof(Administration.LanguagePage), icon: "fa-comments")]
[assembly: NavigationLink(10200, "Yönetim/Çeviriler", typeof(Administration.TranslationPage), icon: "fa-comment-o")]
[assembly: NavigationLink(10300, "Yönetim/Roller", typeof(Administration.RolePage), icon: "fa-lock")]
[assembly: NavigationLink(10400, "Yönetim/Kullanıcı Yönetimi", typeof(Administration.UserPage), icon: "fa-users")]
[assembly: NavigationLink(10500, "Yönetim/İşlem Logları", typeof(Administration.AuditLogPage), icon: "fa-history")]
