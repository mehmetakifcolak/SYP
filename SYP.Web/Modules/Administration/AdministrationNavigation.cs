using Administration = SYP.Administration.Pages;

[assembly: NavigationMenu(7000, "Yönetim", icon: "fa-cogs")]
[assembly: NavigationLink(7100, "Yönetim/Diller", typeof(Administration.LanguagePage), icon: "fa-comments")]
[assembly: NavigationLink(7200, "Yönetim/Çeviriler", typeof(Administration.TranslationPage), icon: "fa-comment-o")]
[assembly: NavigationLink(7300, "Yönetim/Roller", typeof(Administration.RolePage), icon: "fa-lock")]
[assembly: NavigationLink(7400, "Yönetim/Kullanıcı Yönetimi", typeof(Administration.UserPage), icon: "fa-users")]
[assembly: NavigationLink(7500, "Yönetim/İşlem Logları", typeof(Administration.AuditLogPage), icon: "fa-history")]
