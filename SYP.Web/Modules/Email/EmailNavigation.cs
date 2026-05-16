using Serenity.Navigation;
using MyPages = SYP.Email.Pages;

[assembly: NavigationMenu(6000, "Email", icon: "fa-envelope")]
[assembly: NavigationLink(6100, "Email/SMTP Ayarları", typeof(MyPages.SmtpSettingsPage), icon: "fa-server")]
[assembly: NavigationLink(6200, "Email/Şablonlar", typeof(MyPages.EmailTemplatesPage), icon: "fa-file-text")]
[assembly: NavigationLink(6300, "Email/Email Kuyruğu", typeof(MyPages.EmailQueuePage), icon: "fa-list")]
[assembly: NavigationLink(6400, "Email/Gönderim Logları", typeof(MyPages.EmailLogsPage), icon: "fa-history")]
[assembly: NavigationLink(6500, "Email/Ekler", typeof(MyPages.EmailAttachmentsPage), icon: "fa-paperclip")]
