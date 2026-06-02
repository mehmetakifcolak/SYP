-- =============================================================================
-- Email şablonu: MAIL_KULLANICI_GIRIS_BILGILERI
-- Kullanıcılar grid'inden "Giriş Bilgileri Gönder" ile tetiklenen şablon.
--
-- NOT: Projede FluentMigrator migration'ları DataMigrations.Initialize()
-- içinde devre dışı (RunMigrations çağrısı yorum satırı). Bu yüzden
-- DefaultDB_20260602_1000_UserCredentialsEmailTemplate.cs migration'ı veritabanına
-- otomatik uygulanmaz. Şablonu eklemek için bu script'i Default veritabanında
-- bir kez çalıştırın.
--
-- Idempotent: TemplateKey zaten varsa tekrar eklemez.
-- =============================================================================

IF NOT EXISTS (SELECT 1 FROM [dbo].[EmailTemplates] WHERE [TemplateKey] = N'MAIL_KULLANICI_GIRIS_BILGILERI')
BEGIN
    INSERT INTO [dbo].[EmailTemplates]
        ([TemplateKey], [Name], [Subject], [Body], [BodyText],
         [LanguageId], [Category], [Description], [IsActive], [InsertDate], [InsertUserId])
    VALUES
    (
        N'MAIL_KULLANICI_GIRIS_BILGILERI',
        N'Kullanıcı Giriş Bilgileri',
        N'Hesabınız Oluşturuldu - Giriş Bilgileriniz',
        N'<h2>Hesabınız Oluşturuldu</h2>
<p>Sayın {{ad_soyad}},</p>
<p>Sistemde sizin için bir kullanıcı hesabı oluşturuldu. Aşağıdaki giriş bilgileri ile sisteme giriş yapabilirsiniz:</p>
<p><strong>Kullanıcı Adı:</strong> {{kullanici_adi}}<br>
<strong>Geçici Şifre:</strong> {{sifre}}</p>
<p>Güvenliğiniz için giriş yaptıktan sonra şifrenizi değiştirmenizi öneririz.</p>
<p><a href=''{{giris_link}}'' style=''display:inline-block;padding:10px 20px;background-color:#007bff;color:#fff;text-decoration:none;border-radius:4px;''>Giriş Yap</a></p>
<p>İyi çalışmalar dileriz.</p>',
        N'Sayın {{ad_soyad}}, sistemde sizin için bir kullanıcı hesabı oluşturuldu. Kullanıcı Adı: {{kullanici_adi}} Geçici Şifre: {{sifre}} Giriş: {{giris_link}}',
        N'tr',
        N'Kullanıcı',
        N'Kullanıcılar grid''inden giriş bilgileri gönderildiğinde kullanıcıya iletilir',
        1,
        GETDATE(),
        1
    );

    PRINT 'MAIL_KULLANICI_GIRIS_BILGILERI şablonu eklendi.';
END
ELSE
BEGIN
    PRINT 'MAIL_KULLANICI_GIRIS_BILGILERI şablonu zaten mevcut, işlem yapılmadı.';
END
GO
