using FluentMigrator;
using System;

namespace SYP.Migrations.DefaultDB;

[DefaultDB, MigrationKey(20260517_1100)]
public class DefaultDB_20260517_1100_OrderEmailTemplates : AutoReversingMigration
{
    public override void Up()
    {
        // MAIL_YENI_TALEP
        Insert.IntoTable("EmailTemplates").Row(new
        {
            TemplateKey = "MAIL_YENI_TALEP",
            Name = "Yeni Sipariş Talebi",
            Subject = "Yeni Sipariş Talebi - {{siparis_no}}",
            Body = @"<h2>Yeni Sipariş Talebi Alındı</h2>
<p>Sayın Yönetici,</p>
<p><strong>{{bayi_adi}}</strong> bayisi tarafından <strong>{{siparis_no}}</strong> numaralı yeni bir sipariş talebi oluşturuldu.</p>
<p><strong>Toplam Tutar:</strong> {{toplam_tutar}}</p>
<p><strong>Açıklama:</strong> {{aciklama}}</p>
<p><a href='{{siparis_link}}' style='display:inline-block;padding:10px 20px;background-color:#007bff;color:#fff;text-decoration:none;border-radius:4px;'>Sipariş Detayını İncele</a></p>
<p>İyi çalışmalar dileriz.</p>",
            BodyText = "Sayın Yönetici, {{bayi_adi}} bayisi tarafından {{siparis_no}} numaralı yeni bir sipariş talebi oluşturuldu. Toplam Tutar: {{toplam_tutar}}",
            LanguageId = "tr",
            Category = "B2B Sipariş",
            Description = "Bayi yeni sipariş talebi oluşturduğunda yöneticiye gönderilir",
            IsActive = true,
            InsertDate = DateTime.Now,
            InsertUserId = 1
        });

        // MAIL_REVIZE_EDILDI
        Insert.IntoTable("EmailTemplates").Row(new
        {
            TemplateKey = "MAIL_REVIZE_EDILDI",
            Name = "Sipariş Revize Edildi",
            Subject = "Siparişiniz Revize Edildi - {{siparis_no}}",
            Body = @"<h2>Siparişiniz Revize Edildi</h2>
<p>Sayın {{bayi_adi}},</p>
<p><strong>{{siparis_no}}</strong> numaralı siparişiniz yöneticiniz tarafından revize edildi.</p>
<p><strong>Yeni Durum:</strong> {{durum}}</p>
<p><strong>Açıklama:</strong> {{aciklama}}</p>
<p>Lütfen revize edilmiş sipariş detaylarını inceleyiniz ve onaylayınız.</p>
<p><a href='{{siparis_link}}' style='display:inline-block;padding:10px 20px;background-color:#28a745;color:#fff;text-decoration:none;border-radius:4px;'>Revize Detayını İncele ve Onayla</a></p>
<p>İyi günler dileriz.</p>",
            BodyText = "Sayın {{bayi_adi}}, {{siparis_no}} numaralı siparişiniz revize edildi. Lütfen inceleyin ve onaylayın.",
            LanguageId = "tr",
            Category = "B2B Sipariş",
            Description = "Yönetici sipariş revize ettiğinde bayiye gönderilir",
            IsActive = true,
            InsertDate = DateTime.Now,
            InsertUserId = 1
        });

        // MAIL_BAYI_ONAYLADI
        Insert.IntoTable("EmailTemplates").Row(new
        {
            TemplateKey = "MAIL_BAYI_ONAYLADI",
            Name = "Bayi Siparişi Onayladı",
            Subject = "Sipariş Onaylandı - {{siparis_no}}",
            Body = @"<h2>Sipariş Onaylandı</h2>
<p>Sayın Yönetici,</p>
<p><strong>{{bayi_adi}}</strong> bayisi <strong>{{siparis_no}}</strong> numaralı siparişi onayladı.</p>
<p><strong>Toplam Tutar:</strong> {{toplam_tutar}}</p>
<p>Bayi ödeme dekontunu yükleyecektir. Dekont yüklendiğinde size bildirim gönderilecektir.</p>
<p><a href='{{siparis_link}}' style='display:inline-block;padding:10px 20px;background-color:#007bff;color:#fff;text-decoration:none;border-radius:4px;'>Sipariş Detayını Görüntüle</a></p>
<p>İyi çalışmalar dileriz.</p>",
            BodyText = "Sayın Yönetici, {{bayi_adi}} bayisi {{siparis_no}} numaralı siparişi onayladı. Toplam Tutar: {{toplam_tutar}}",
            LanguageId = "tr",
            Category = "B2B Sipariş",
            Description = "Bayi sipariş onayladığında yöneticiye gönderilir",
            IsActive = true,
            InsertDate = DateTime.Now,
            InsertUserId = 1
        });

        // MAIL_BAYI_REDDETTI
        Insert.IntoTable("EmailTemplates").Row(new
        {
            TemplateKey = "MAIL_BAYI_REDDETTI",
            Name = "Bayi Siparişi Reddetti",
            Subject = "Sipariş Reddedildi - {{siparis_no}}",
            Body = @"<h2>Sipariş Reddedildi</h2>
<p>Sayın Yönetici,</p>
<p><strong>{{bayi_adi}}</strong> bayisi <strong>{{siparis_no}}</strong> numaralı siparişi reddetti.</p>
<p><strong>Red Nedeni:</strong> {{aciklama}}</p>
<p>Lütfen bayi ile iletişime geçin veya siparişi yeniden revize edin.</p>
<p><a href='{{siparis_link}}' style='display:inline-block;padding:10px 20px;background-color:#dc3545;color:#fff;text-decoration:none;border-radius:4px;'>Sipariş Detayını İncele</a></p>
<p>İyi çalışmalar dileriz.</p>",
            BodyText = "Sayın Yönetici, {{bayi_adi}} bayisi {{siparis_no}} numaralı siparişi reddetti. Red Nedeni: {{aciklama}}",
            LanguageId = "tr",
            Category = "B2B Sipariş",
            Description = "Bayi sipariş reddettiğinde yöneticiye gönderilir",
            IsActive = true,
            InsertDate = DateTime.Now,
            InsertUserId = 1
        });

        // MAIL_DEKONT_YUKLENDI
        Insert.IntoTable("EmailTemplates").Row(new
        {
            TemplateKey = "MAIL_DEKONT_YUKLENDI",
            Name = "Ödeme Dekontu Yüklendi",
            Subject = "Ödeme Dekontu Yüklendi - {{siparis_no}}",
            Body = @"<h2>Ödeme Dekontu Yüklendi</h2>
<p>Sayın Yönetici,</p>
<p><strong>{{bayi_adi}}</strong> bayisi <strong>{{siparis_no}}</strong> numaralı sipariş için ödeme dekontunu yükledi.</p>
<p><strong>Toplam Tutar:</strong> {{toplam_tutar}}</p>
<p>Lütfen dekontu inceleyin ve onaylayın.</p>
<p><a href='{{siparis_link}}' style='display:inline-block;padding:10px 20px;background-color:#ffc107;color:#000;text-decoration:none;border-radius:4px;'>Dekontu İncele ve Onayla</a></p>
<p>İyi çalışmalar dileriz.</p>",
            BodyText = "Sayın Yönetici, {{bayi_adi}} bayisi {{siparis_no}} numaralı sipariş için ödeme dekontunu yükledi. Lütfen inceleyin.",
            LanguageId = "tr",
            Category = "B2B Sipariş",
            Description = "Bayi dekont yüklediğinde yöneticiye gönderilir",
            IsActive = true,
            InsertDate = DateTime.Now,
            InsertUserId = 1
        });

        // MAIL_DEKONT_REDDEDILDI
        Insert.IntoTable("EmailTemplates").Row(new
        {
            TemplateKey = "MAIL_DEKONT_REDDEDILDI",
            Name = "Ödeme Dekontu Reddedildi",
            Subject = "Ödeme Dekontu Reddedildi - {{siparis_no}}",
            Body = @"<h2>Ödeme Dekontu Reddedildi</h2>
<p>Sayın {{bayi_adi}},</p>
<p><strong>{{siparis_no}}</strong> numaralı siparişiniz için yüklediğiniz ödeme dekontu reddedildi.</p>
<p><strong>Red Nedeni:</strong> {{aciklama}}</p>
<p>Lütfen geçerli bir dekont yükleyiniz.</p>
<p><a href='{{siparis_link}}' style='display:inline-block;padding:10px 20px;background-color:#dc3545;color:#fff;text-decoration:none;border-radius:4px;'>Yeni Dekont Yükle</a></p>
<p>İyi günler dileriz.</p>",
            BodyText = "Sayın {{bayi_adi}}, {{siparis_no}} numaralı siparişiniz için yüklediğiniz dekont reddedildi. Red Nedeni: {{aciklama}}",
            LanguageId = "tr",
            Category = "B2B Sipariş",
            Description = "Yönetici dekontu reddettiğinde bayiye gönderilir",
            IsActive = true,
            InsertDate = DateTime.Now,
            InsertUserId = 1
        });

        // MAIL_HAZIRLANIYOR
        Insert.IntoTable("EmailTemplates").Row(new
        {
            TemplateKey = "MAIL_HAZIRLANIYOR",
            Name = "Sipariş Hazırlanıyor",
            Subject = "Siparişiniz Hazırlanıyor - {{siparis_no}}",
            Body = @"<h2>Siparişiniz Hazırlanıyor</h2>
<p>Sayın {{bayi_adi}},</p>
<p><strong>{{siparis_no}}</strong> numaralı siparişinizin ödemesi onaylandı ve sipariş hazırlanmaya başlandı.</p>
<p><strong>Toplam Tutar:</strong> {{toplam_tutar}}</p>
<p>Siparişiniz en kısa sürede kargoya verilecektir. Kargo bilgileri tarafınıza ayrıca iletilecektir.</p>
<p><a href='{{siparis_link}}' style='display:inline-block;padding:10px 20px;background-color:#17a2b8;color:#fff;text-decoration:none;border-radius:4px;'>Sipariş Durumunu Takip Et</a></p>
<p>İyi günler dileriz.</p>",
            BodyText = "Sayın {{bayi_adi}}, {{siparis_no}} numaralı siparişiniz hazırlanmaya başlandı. Toplam Tutar: {{toplam_tutar}}",
            LanguageId = "tr",
            Category = "B2B Sipariş",
            Description = "Yönetici dekontu onaylayıp sipariş hazırlanmaya başladığında bayiye gönderilir",
            IsActive = true,
            InsertDate = DateTime.Now,
            InsertUserId = 1
        });

        // MAIL_SEVK_ASAMASINDA
        Insert.IntoTable("EmailTemplates").Row(new
        {
            TemplateKey = "MAIL_SEVK_ASAMASINDA",
            Name = "Sipariş Kargoya Verildi",
            Subject = "Siparişiniz Kargoya Verildi - {{siparis_no}}",
            Body = @"<h2>Siparişiniz Kargoya Verildi</h2>
<p>Sayın {{bayi_adi}},</p>
<p><strong>{{siparis_no}}</strong> numaralı siparişiniz kargoya verildi.</p>
<p><strong>Toplam Tutar:</strong> {{toplam_tutar}}</p>
<p><strong>Kargo Bilgileri:</strong> {{aciklama}}</p>
<p>Kargo takip numaranız ve detayları yakında size iletilecektir.</p>
<p><a href='{{siparis_link}}' style='display:inline-block;padding:10px 20px;background-color:#28a745;color:#fff;text-decoration:none;border-radius:4px;'>Sipariş Durumunu Görüntüle</a></p>
<p>İyi günler dileriz.</p>",
            BodyText = "Sayın {{bayi_adi}}, {{siparis_no}} numaralı siparişiniz kargoya verildi. Kargo Bilgileri: {{aciklama}}",
            LanguageId = "tr",
            Category = "B2B Sipariş",
            Description = "Sipariş kargoya verildiğinde bayiye gönderilir",
            IsActive = true,
            InsertDate = DateTime.Now,
            InsertUserId = 1
        });

        // MAIL_TESLIM_EDILDI
        Insert.IntoTable("EmailTemplates").Row(new
        {
            TemplateKey = "MAIL_TESLIM_EDILDI",
            Name = "Sipariş Teslim Edildi",
            Subject = "Siparişiniz Teslim Edildi - {{siparis_no}}",
            Body = @"<h2>Siparişiniz Teslim Edildi</h2>
<p>Sayın {{bayi_adi}},</p>
<p><strong>{{siparis_no}}</strong> numaralı siparişiniz teslim edildi.</p>
<p><strong>Toplam Tutar:</strong> {{toplam_tutar}}</p>
<p>Siparişinizin eksiksiz ve hasarsız teslim edildiğini umuyoruz. Herhangi bir sorun yaşarsanız lütfen bizimle iletişime geçin.</p>
<p><a href='{{siparis_link}}' style='display:inline-block;padding:10px 20px;background-color:#007bff;color:#fff;text-decoration:none;border-radius:4px;'>Sipariş Detaylarını Görüntüle</a></p>
<p>İşbirliğiniz için teşekkür ederiz. İyi günler dileriz.</p>",
            BodyText = "Sayın {{bayi_adi}}, {{siparis_no}} numaralı siparişiniz teslim edildi. Teşekkür ederiz.",
            LanguageId = "tr",
            Category = "B2B Sipariş",
            Description = "Sipariş teslim edildiğinde bayiye gönderilir",
            IsActive = true,
            InsertDate = DateTime.Now,
            InsertUserId = 1
        });

        // MAIL_TALEP_IPTAL
        Insert.IntoTable("EmailTemplates").Row(new
        {
            TemplateKey = "MAIL_TALEP_IPTAL",
            Name = "Sipariş İptal Edildi",
            Subject = "Sipariş İptal Edildi - {{siparis_no}}",
            Body = @"<h2>Sipariş İptal Edildi</h2>
<p>Sayın {{bayi_adi}},</p>
<p><strong>{{siparis_no}}</strong> numaralı siparişiniz iptal edildi.</p>
<p><strong>İptal Nedeni:</strong> {{aciklama}}</p>
<p><strong>İptal Eden:</strong> {{degistiren_kullanici}}</p>
<p>Herhangi bir sorunuz veya itirazınız varsa lütfen bizimle iletişime geçin.</p>
<p><a href='{{siparis_link}}' style='display:inline-block;padding:10px 20px;background-color:#6c757d;color:#fff;text-decoration:none;border-radius:4px;'>İptal Edilen Siparişi Görüntüle</a></p>
<p>İyi günler dileriz.</p>",
            BodyText = "Sayın {{bayi_adi}}, {{siparis_no}} numaralı siparişiniz iptal edildi. İptal Nedeni: {{aciklama}}",
            LanguageId = "tr",
            Category = "B2B Sipariş",
            Description = "Sipariş iptal edildiğinde karşı tarafa gönderilir",
            IsActive = true,
            InsertDate = DateTime.Now,
            InsertUserId = 1
        });
    }
}
