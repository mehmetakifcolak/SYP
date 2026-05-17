using Serenity.Navigation;
using MyPages = SYP.Email.Pages;

[assembly: NavigationMenu(6000, "Email", icon: "fa-envelope")]
[assembly: NavigationLink(6100, "Email/SMTP Settings", typeof(MyPages.SmtpSettingsPage), icon: "fa-server")]
[assembly: NavigationLink(6200, "Email/Templates", typeof(MyPages.EmailTemplatesPage), icon: "fa-file-text")]
[assembly: NavigationLink(6300, "Email/Email Queue", typeof(MyPages.EmailQueuePage), icon: "fa-list")]
[assembly: NavigationLink(6400, "Email/Delivery Logs", typeof(MyPages.EmailLogsPage), icon: "fa-history")]
[assembly: NavigationLink(6500, "Email/Attachments", typeof(MyPages.EmailAttachmentsPage), icon: "fa-paperclip")]
