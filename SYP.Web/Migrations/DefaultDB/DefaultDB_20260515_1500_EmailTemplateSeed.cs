using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[Migration(20260515_1500)]
public class DefaultDB_20260515_1500_EmailTemplateSeed : Migration
{
    public override void Up()
    {
        // Yeni Müşteri Hoşgeldin Email Şablonu
        Insert.IntoTable("EmailTemplates").Row(new
        {
            TemplateKey = "NEW_CUSTOMER_WELCOME",
            Name = "Yeni Müşteri Hoşgeldin",
            Subject = "Hoş Geldiniz - {{CompanyName}}",
            Body = @"
<html>
<head>
    <style>
        body { font-family: Arial, sans-serif; line-height: 1.6; color: #333; }
        .container { max-width: 600px; margin: 0 auto; padding: 20px; }
        .header { background: #4a90d9; color: white; padding: 20px; text-align: center; }
        .content { padding: 20px; background: #f9f9f9; }
        .credentials { background: #fff; border: 1px solid #ddd; padding: 15px; margin: 15px 0; }
        .footer { text-align: center; padding: 20px; font-size: 12px; color: #666; }
        .button { display: inline-block; background: #4a90d9; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px; }
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>Hoş Geldiniz!</h1>
        </div>
        <div class='content'>
            <p>Sayın <strong>{{CustomerName}}</strong>,</p>
            <p>{{CompanyName}} ailesine hoş geldiniz! Hesabınız başarıyla oluşturulmuştur.</p>

            <div class='credentials'>
                <h3>Giriş Bilgileriniz:</h3>
                <p><strong>Kullanıcı Adı:</strong> {{Username}}</p>
                <p><strong>Şifre:</strong> {{Password}}</p>
            </div>

            <p>Güvenliğiniz için lütfen ilk girişinizde şifrenizi değiştirin.</p>

            <p style='text-align: center; margin-top: 30px;'>
                <a href='{{LoginUrl}}' class='button'>Sisteme Giriş Yap</a>
            </p>
        </div>
        <div class='footer'>
            <p>Bu email otomatik olarak gönderilmiştir. Lütfen yanıtlamayınız.</p>
            <p>&copy; {{Year}} {{CompanyName}}. Tüm hakları saklıdır.</p>
        </div>
    </div>
</body>
</html>",
            BodyText = @"Sayın {{CustomerName}},

{{CompanyName}} ailesine hoş geldiniz! Hesabınız başarıyla oluşturulmuştur.

Giriş Bilgileriniz:
Kullanıcı Adı: {{Username}}
Şifre: {{Password}}

Güvenliğiniz için lütfen ilk girişinizde şifrenizi değiştirin.

Sisteme giriş yapmak için: {{LoginUrl}}

Bu email otomatik olarak gönderilmiştir.
© {{Year}} {{CompanyName}}. Tüm hakları saklıdır.",
            LanguageId = "tr",
            Category = "Müşteri",
            Description = "Yeni müşteri kaydı oluşturulduğunda gönderilen hoşgeldin emaili",
            IsActive = true,
            InsertDate = DateTime.Now,
            InsertUserId = 1
        });
    }

    public override void Down()
    {
        Delete.FromTable("EmailTemplates").Row(new { TemplateKey = "NEW_CUSTOMER_WELCOME" });
    }
}
