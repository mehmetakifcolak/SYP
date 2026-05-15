using Serenity.Navigation;
using MyPages = SYP.Email.Pages;

[assembly: NavigationMenu(9000, "Email", icon: "fa-envelope")]
[assembly: NavigationLink(9100, "Email/SMTP Ayarları", typeof(MyPages.SmtpSettingsPage), icon: "fa-server")]
[assembly: NavigationLink(9200, "Email/Şablonlar", typeof(MyPages.EmailTemplatesPage), icon: "fa-file-text")]
[assembly: NavigationLink(9300, "Email/Email Kuyruğu", typeof(MyPages.EmailQueuePage), icon: "fa-list")]
[assembly: NavigationLink(9400, "Email/Gönderim Logları", typeof(MyPages.EmailLogsPage), icon: "fa-history")]
[assembly: NavigationLink(9500, "Email/Ekler", typeof(MyPages.EmailAttachmentsPage), icon: "fa-paperclip")]
