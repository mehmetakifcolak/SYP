using FluentMigrator;
using System;

namespace SYP.Migrations.DefaultDB;

[DefaultDB, MigrationKey(20260602_1000)]
public class DefaultDB_20260602_1000_UserCredentialsEmailTemplate : AutoReversingMigration
{
    public override void Up()
    {
        // MAIL_KULLANICI_GIRIS_BILGILERI
        Insert.IntoTable("EmailTemplates").Row(new
        {
            TemplateKey = "MAIL_KULLANICI_GIRIS_BILGILERI",
            Name = "Kullanıcı Giriş Bilgileri",
            Subject = "Hesabınız Oluşturuldu - Giriş Bilgileriniz",
            Body = @"<h2>Hesabınız Oluşturuldu</h2>
<p>Sayın {{ad_soyad}},</p>
<p>Sistemde sizin için bir kullanıcı hesabı oluşturuldu. Aşağıdaki giriş bilgileri ile sisteme giriş yapabilirsiniz:</p>
<p><strong>Kullanıcı Adı:</strong> {{kullanici_adi}}<br>
<strong>Geçici Şifre:</strong> {{sifre}}</p>
<p>Güvenliğiniz için giriş yaptıktan sonra şifrenizi değiştirmenizi öneririz.</p>
<p><a href='{{giris_link}}' style='display:inline-block;padding:10px 20px;background-color:#007bff;color:#fff;text-decoration:none;border-radius:4px;'>Giriş Yap</a></p>
<p>İyi çalışmalar dileriz.</p>",
            BodyText = "Sayın {{ad_soyad}}, sistemde sizin için bir kullanıcı hesabı oluşturuldu. Kullanıcı Adı: {{kullanici_adi}} Geçici Şifre: {{sifre}} Giriş: {{giris_link}}",
            LanguageId = "tr",
            Category = "Kullanıcı",
            Description = "Kullanıcılar grid'inden giriş bilgileri gönderildiğinde kullanıcıya iletilir",
            IsActive = true,
            InsertDate = DateTime.Now,
            InsertUserId = 1
        });
    }
}
