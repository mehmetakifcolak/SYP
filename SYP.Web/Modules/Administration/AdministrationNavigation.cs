using Administration = SYP.Administration.Pages;

[assembly: NavigationMenu(7000, "Administration", icon: "fa-cogs")]
[assembly: NavigationLink(7100, "Administration/Languages", typeof(Administration.LanguagePage), icon: "fa-comments")]
[assembly: NavigationLink(7200, "Administration/Translations", typeof(Administration.TranslationPage), icon: "fa-comment-o")]
[assembly: NavigationLink(7300, "Administration/Roles", typeof(Administration.RolePage), icon: "fa-lock")]
[assembly: NavigationLink(7400, "Administration/User Management", typeof(Administration.UserPage), icon: "fa-users")]
[assembly: NavigationLink(7500, "Administration/Audit Logs", typeof(Administration.AuditLogPage), icon: "fa-history")]
