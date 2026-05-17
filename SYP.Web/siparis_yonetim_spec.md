# Sipariş Yönetim Portalı — Gereksinim Belgesi

## Genel Tanım

Bu uygulama, bayilerin kullanıcı adı ve şifreleriyle giriş yaparak sipariş talebinde bulunduğu bir **B2B Sipariş Yönetim Portalı**'dır. Klasik e-ticaretten farklı olarak sipariş süreci **müzakere tabanlı (Proforma Model)** çalışır: bayi sipariş talebini iletir, yönetici revize eder, bayi onaylar, ödeme yapılır.

---

## Kullanıcı Rolleri

| Rol | Açıklama |
|---|---|
| **Bayi** | Ürün seçer, sipariş talebi oluşturur, onay/ödeme adımlarını takip eder |
| **Sorumlu Yönetici** | Kendisine atanmış bayilerin taleplerini inceler, revize eder, siparişi yönetir |
| **Süper Admin** | Tüm bayileri ve siparişleri görür; yönetici–bayi ataması yapar; sistem ayarlarını yönetir |

---

## Bayi — Sorumlu Yönetici Ataması

### Atama Modeli
Her bayi hesabına sistemde tanımlı **bir Sorumlu Yönetici** atanır. Bu atama Süper Admin tarafından yapılır.

```
Bayi A  →  Yönetici Ahmet
Bayi B  →  Yönetici Ahmet
Bayi C  →  Yönetici Mehmet
Bayi D  →  Yönetici Mehmet
```

- Bir yöneticiye birden fazla bayi atanabilir
- Bir bayinin yalnızca **tek** sorumlu yöneticisi olur
- Atama değiştirilebilir; değişiklik mevcut açık siparişleri etkilemez (eski yönetici üzerinde kalır) ya da yeni yöneticiye devredilir — tercih edilecek davranış tanımlanmalı

### Etkisi

| Konu | Davranış |
|---|---|
| Sipariş görünürlüğü | Yönetici yalnızca kendi bayilerinin siparişlerini görür |
| Mail bildirimi | Bayi sipariş ilettiğinde yalnızca atanmış yöneticiye mail gider |
| Revizyon yetkisi | Yalnızca atanmış yönetici revize edebilir |
| Dekont onayı | Yalnızca atanmış yönetici onaylayabilir |
| Lojistik güncelleme | Yalnızca atanmış yönetici durum güncelleyebilir |
| Süper Admin | Tüm bayilerin tüm siparişlerini her zaman görür ve müdahale edebilir |

### Atama Ekranı (Süper Admin Paneli)
- Bayi listesi üzerinden her bayinin yanında "Sorumlu Yönetici" dropdown'u
- Toplu atama: birden fazla bayiyi seçip tek yöneticiye atayabilme
- Yönetici bazında görünüm: "Ahmet'e atanmış bayiler" listesi
- Atanmamış bayi uyarısı: sorumlusu olmayan bayiler kırmızı / uyarı işaretiyle listelenir

---

## Sipariş İş Akışı (6 Aşama)

### Aşama 1 — Talep Oluşturma (Bayi)
- Bayi, ürün kataloğundan ürün seçer
- Ürünler listelenirken **ürün görselleri** de görünür
- Seçilen ürünler sepete eklenir
- Bayi sepeti "Sipariş Talebi" olarak sisteme iletir
- Bu aşamada sipariş durumu: **`TALEP_GONDERILDI`**

### Aşama 2 — Yönetici İncelemesi (Admin)
- Admin, gelen talebi açar
- Stok durumuna göre şunları yapabilir:
  - Ürün miktarlarını revize etmek
  - Birim fiyatları güncellemek
  - Alt toplama ekstra indirim uygulamak
- Revize edilen teklif bayiye gönderilir
- Sipariş durumu: **`REVIZE_EDILDI`**

### Aşama 3 — Bayi Onayı (Bayi)
- Bayi, revize edilmiş ürün listesini ve fiyatları görür
- Kabul ederse **"Onayla ve Ödemeye Geç"** butonuna tıklar
- Reddetme veya değişiklik talep etme hakkı da olmalı (opsiyonel yorum alanı)
- Sipariş durumu: **`BAYI_ONAYLADI`**

### Aşama 4 — Ödeme ve Dekont (Bayi)
- Bayi, ödeme ekranında şirketin **banka hesap bilgilerini** görür (IBAN, banka adı, hesap sahibi)
- Ödemeyi banka üzerinden yapar
- Dekont görselini (JPG/PNG/PDF) sisteme yükler
- Sipariş durumu: **`DEKONT_YUKLENDI`**

### Aşama 5 — Kesin Onay ve Stok Düşümü (Admin)
- Admin yüklenen dekontu inceler ve onaylar
- Onay anında:
  - Sipariş durumu **`HAZIRLANIYOR`**'a geçer
  - İlgili ürünlerin stok miktarları depodan düşülür

### Aşama 6 — Lojistik Takibi (Admin → Bayi)
- Admin sipariş durumunu manuel olarak günceller:
  - `HAZIRLANIYOR` → `SEVK_ASAMASINDA` → `TESLIM_EDILDI`
- Bayi kendi panelinden anlık durumu görebilir

---

## Durum Akış Şeması

```
                        TALEP_GONDERILDI
                        ↓              ↓
              (Admin revize eder)   (Admin iptal eder)
                        ↓              ↓
                 REVIZE_EDILDI     TALEP_IPTAL ──→ [terminal]
                 ↓          ↓
       (Bayi onaylar)  (Bayi reddeder)
                 ↓          ↓
        BAYI_ONAYLADI   BAYI_REDDETTI
              ↓                ↓
   (Bayi dekont yükler)  (Admin yeniden revize eder → REVIZE_EDILDI)
              ↓                veya (Admin iptal eder → TALEP_IPTAL)
       DEKONT_YUKLENDI
         ↓          ↓
(Admin onaylar) (Admin reddeder)
         ↓          ↓
  HAZIRLANIYOR  DEKONT_REDDEDILDI
         ↓          ↓
         ↓      (Bayi yeni dekont yükler → DEKONT_YUKLENDI)
         ↓          veya (Bayi vazgeçer → TALEP_IPTAL)
  SEVK_ASAMASINDA
         ↓
  TESLIM_EDILDI ──→ [terminal]
```

---

## Reddetme ve İptal Durumları

### `BAYI_REDDETTI` — Bayi Revizyonu Reddetti

**Ne zaman:** Bayi, Aşama 3'te yöneticinin revize ettiği teklifi kabul etmez.

**Kim tetikler:** Bayi

**Akış:**
- Bayi talep detayında **"Reddet"** butonuna tıklar
- Zorunlu **red nedeni / yorum** alanı doldurulur
- Sipariş `BAYI_REDDETTI` durumuna geçer
- Sorumlu Yöneticiye `MAIL_BAYI_REDDETTI` şablonuyla mail gönderilir

**Sonrasında ne olur — 2 seçenek (yönetici karar verir):**

| Seçenek | Açıklama | Yeni Durum |
|---|---|---|
| Yeniden revize et | Yönetici yorumu okur, teklifi günceller, tekrar bayiye gönderir | `REVIZE_EDILDI` |
| İptal et | Müzakere sonuçsuz kalır, sipariş kapatılır | `TALEP_IPTAL` |

---

### `DEKONT_REDDEDILDI` — Admin Dekontu Reddetti

**Ne zaman:** Admin, Aşama 5'te yüklenen dekontu geçersiz / hatalı bulur.

**Kim tetikler:** Sorumlu Yönetici veya Süper Admin

**Akış:**
- Admin dekont inceleme ekranında **"Reddet"** butonuna tıklar
- Zorunlu **red nedeni** girilir (örn. "Tutar eksik", "Okunamıyor", "Yanlış hesap")
- Sipariş `DEKONT_REDDEDILDI` durumuna geçer
- Bayiye `MAIL_DEKONT_REDDEDILDI` şablonuyla red nedeni belirtilerek mail gönderilir

**Sonrasında ne olur — 2 seçenek (bayi karar verir):**

| Seçenek | Açıklama | Yeni Durum |
|---|---|---|
| Yeni dekont yükle | Bayi doğru dekontu yükler | `DEKONT_YUKLENDI` |
| Vazgeç / iptal et | Bayi siparişi sonlandırır | `TALEP_IPTAL` |

---

### `TALEP_IPTAL` — Sipariş İptal Edildi

**Ne zaman:** Süreç herhangi bir aşamada sonlandırılır. Terminal durumdur, geri dönüşü yoktur.

**Kim tetikleyebilir:**

| Tetikleyen | Hangi Aşamada |
|---|---|
| Admin | `TALEP_GONDERILDI` veya `BAYI_REDDETTI` aşamasında |
| Bayi | `DEKONT_REDDEDILDI` aşamasında veya talebi kendisi geri çekerse |
| Süper Admin | Her aşamada |

**Akış:**
- İptal eden taraf zorunlu **iptal nedeni** girer
- Karşı tarafa `MAIL_TALEP_IPTAL` şablonuyla bildirim gönderilir (iptal nedeni mailde yer alır)
- Sipariş salt okunur hale gelir, üzerinde işlem yapılamaz
- Stok düşümü yapılmamışsa stok etkilenmez
- İptal edilen sipariş arşivde görünür, silinemez

**Not:** `HAZIRLANIYOR`, `SEVK_ASAMASINDA`, `TESLIM_EDILDI` durumlarından iptal yapılamaz — bu aşamalarda stok zaten düşülmüştür; iade/düzeltme ayrı bir süreçle yönetilmelidir.

---

### Tüm Durumların Özeti

| Durum | Tür | Tetikleyen | Açıklama |
|---|---|---|---|
| `TALEP_GONDERILDI` | Aktif | Bayi | İlk talep iletildi |
| `REVIZE_EDILDI` | Aktif | Yönetici | Yönetici revize etti, bayi onayı bekleniyor |
| `BAYI_ONAYLADI` | Aktif | Bayi | Bayi teklifi kabul etti, dekont bekleniyor |
| `BAYI_REDDETTI` | Aktif | Bayi | Bayi teklifi reddetti, yönetici aksiyonu bekleniyor |
| `DEKONT_YUKLENDI` | Aktif | Bayi | Dekont yüklendi, admin onayı bekleniyor |
| `DEKONT_REDDEDILDI` | Aktif | Yönetici | Dekont geçersiz, bayi yeni dekont bekleniyor |
| `HAZIRLANIYOR` | Aktif | Yönetici | Dekont onaylandı, stok düşüldü, hazırlanıyor |
| `SEVK_ASAMASINDA` | Aktif | Yönetici | Kargoya verildi |
| `TESLIM_EDILDI` | Terminal ✓ | Yönetici | Teslimat tamamlandı |
| `TALEP_IPTAL` | Terminal ✗ | Admin / Bayi | Sipariş iptal edildi |

---

## Ürün Kataloğu ve Sepet

### Ürün Listeleme
- Ürün görseli (thumbnail)
- Ürün adı ve kodu
- Birim fiyatı (bayiye özel fiyat varsa öncelikli gösterilir)
- Stok durumu (var / yok / sınırlı) — **yalnızca Yönetici ve Süper Admin görür, bayi panelinde gösterilmez**
- Arama ve kategori filtresi

### Sepet
- Ürün adı, görseli, miktar, birim fiyat, ara toplam
- Bayi özel iskontosu uygulanmış hali gösterilir
- **Kademeli Sepet İndirimi** otomatik uygulanır (aşağıya bkz.)
- Genel toplam ve toplam indirim özeti
- "Sipariş Talebi Gönder" butonu

---

## Kademeli Sepet İndirimi (Teşvik İndirimi)

Bayinin özel iskontosuna **ek olarak**, sepet toplamına göre otomatik ek indirim uygulanır.

| Eşik Tutar | Ek İndirim Oranı |
|---|---|
| 15.000 TL ve üzeri | %10 |
| 50.000 TL ve üzeri | %15 |

**Kurallar:**
- Eşikler ve oranlar admin panelinden düzenlenebilir olmalıdır
- Bayi özel iskontosu ile kademeli indirim **birbirinden bağımsız** hesaplanır (hangisi önce uygulanacak tanımlanabilmeli)
- Sepette hangi eşiğin tetiklendiği ve ne kadar ek indirim uygulandığı kullanıcıya açıkça gösterilmelidir

---

## Bayi Paneli — Temel Ekranlar

1. **Dashboard** — Aktif talep sayısı, son siparişler, bekleyen aksiyonlar
2. **Ürün Kataloğu** — Görsel destekli ürün listeleme ve sepet
3. **Sipariş Taleplerim** — Tüm siparişlerin durumu ve geçmiş
4. **Talep Detayı** — Revize edilmiş liste görüntüleme, onay/red, dekont yükleme
5. **Banka Bilgileri** — Ödeme ekranında gösterilecek statik bilgiler

## Admin Paneli — Temel Ekranlar

**Sorumlu Yönetici:**
1. **Dashboard** — Kendi bayilerinden gelen bekleyen talepler, dekont onayı bekleyenler
2. **Gelen Talepler** — Yalnızca kendi bayilerinin talep listesi ve durum filtresi
3. **Talep Detayı** — Revizyon yapma, fiyat/miktar güncelleme, indirim ekleme
4. **Dekont İnceleme** — Görsel önizleme + onay / red
5. **Sipariş Yönetimi** — Durum güncelleme (Hazırlanıyor → Sevk → Teslim)

**Süper Admin (yukarıdakilere ek):**
6. **Tüm Siparişler** — Tüm bayilerin sipariş listesi, yönetici filtresi ile görüntüleme
7. **Bayi — Yönetici Ataması** — Her bayiye sorumlu yönetici atama, toplu atama, atamasız bayi uyarıları
8. **Kullanıcı Yönetimi** — Bayi ve yönetici hesapları CRUD
9. **Kademeli İndirim Ayarları** — Eşik ve oran yönetimi
10. **Stok Yönetimi** — Ürün/stok CRUD
11. **Mail Şablonları** — Şablon düzenleme, önizleme, aktif/pasif
12. **Mail Log** — Gönderim geçmişi, hata kayıtları
13. **SMTP Ayarları** — Mail sunucu yapılandırması

---

## E-posta Bildirim Sistemi

### Genel Kural
Bir sipariş durumu değiştiğinde, **değişikliği yapan kullanıcının karşı tarafına** otomatik e-posta gönderilir. Mail alıcısı, bayiye atanmış **Sorumlu Yönetici** üzerinden belirlenir:

| Değişikliği Yapan | E-posta Alan |
|---|---|
| Bayi | Bayinin Sorumlu Yöneticisi |
| Sorumlu Yönetici | İlgili Bayi kullanıcısı |
| Süper Admin | İlgili Bayi kullanıcısı (+ opsiyonel: Sorumlu Yönetici de CC alabilir) |

> Bayiye henüz Sorumlu Yönetici atanmamışsa, mail `bildirim_alsin = true` işaretli tüm Süper Admin hesaplarına gönderilir ve sistem bu bayiyi "atamasız bayi" olarak uyarı listesine ekler.

---

### Durum → Şablon Eşleştirmesi

Her durum geçişi için sistemde **ayrı bir mail şablonu** tanımlanır. Admin panelinden bu şablonlar düzenlenebilir.

| Durum Geçişi | Tetikleyen | E-posta Alan | Şablon Kodu |
|---|---|---|---|
| `TALEP_GONDERILDI` | Bayi | Sorumlu Yönetici | `MAIL_YENI_TALEP` |
| `REVIZE_EDILDI` | Yönetici | Bayi | `MAIL_REVIZE_EDILDI` |
| `BAYI_ONAYLADI` | Bayi | Sorumlu Yönetici | `MAIL_BAYI_ONAYLADI` |
| `BAYI_REDDETTI` | Bayi | Sorumlu Yönetici | `MAIL_BAYI_REDDETTI` |
| `DEKONT_YUKLENDI` | Bayi | Sorumlu Yönetici | `MAIL_DEKONT_YUKLENDI` |
| `DEKONT_REDDEDILDI` | Yönetici | Bayi | `MAIL_DEKONT_REDDEDILDI` |
| `HAZIRLANIYOR` | Yönetici | Bayi | `MAIL_HAZIRLANIYOR` |
| `SEVK_ASAMASINDA` | Yönetici | Bayi | `MAIL_SEVK_ASAMASINDA` |
| `TESLIM_EDILDI` | Yönetici | Bayi | `MAIL_TESLIM_EDILDI` |
| `TALEP_IPTAL` | Admin veya Bayi | Karşı taraf | `MAIL_TALEP_IPTAL` |

---

### Mail Şablon Yapısı

Her şablon aşağıdaki alanlardan oluşur:

```
ShablonKodu   : MAIL_REVIZE_EDILDI          (sistem tarafından sabit, değiştirilemez)
Konu          : Siparişiniz revize edildi — #{{siparis_no}}
HTML Gövde    : Zengin metin editörü ile düzenlenebilir
Durum         : Aktif / Pasif
```

**Şablon içinde kullanılabilecek dinamik değişkenler (placeholder):**

| Değişken | Açıklama |
|---|---|
| `{{siparis_no}}` | Sipariş / talep numarası |
| `{{bayi_adi}}` | Bayi firma adı |
| `{{bayi_kullanici}}` | Bayinin kullanıcı adı veya adı soyadı |
| `{{siparis_tarihi}}` | Talebin oluşturulma tarihi |
| `{{toplam_tutar}}` | Sipariş toplam tutarı (TL) |
| `{{durum}}` | Yeni durum (Türkçe okunabilir hali) |
| `{{aciklama}}` | Admin veya bayinin eklediği not/yorum (varsa) |
| `{{siparis_link}}` | Sipariş detay sayfasına doğrudan bağlantı |
| `{{degistiren_kullanici}}` | Durumu değiştiren kişinin adı |

---

### Admin Paneli — Mail Şablonları Ekranı

- Tüm şablonlar listelenir (şablon kodu, konu, son düzenleme tarihi, aktif/pasif)
- Her şablon için **HTML editörü** ile konu ve gövde düzenlenebilir
- Kaydetmeden önce **önizleme** yapılabilir (örnek verilerle render edilmiş hali gösterilir)
- Şablon **aktif/pasif** yapılabilir; pasif şablonda o durum geçişi için mail gönderilmez
- Şablon **sıfırla** seçeneği ile fabrika ayarlarına dönülebilir

---

### Teknik Gereksinimler (Mail)

- Mail gönderimi asenkron / kuyruk tabanlı olmalı (anlık UI işlemini bloke etmemeli)
- SMTP ayarları (host, port, kullanıcı, şifre, from adresi) admin panelinden yapılandırılabilmeli
- Gönderim başarısız olursa yeniden deneme mekanizması olmalı (retry, max 3 deneme)
- Her gönderilen mail log'a yazılmalı: alıcı, şablon kodu, gönderim zamanı, başarı/hata durumu
- Mail log admin panelinden görüntülenebilir olmalı

---

## Teknik Notlar (Claude Code için)

- Kimlik doğrulama: JWT tabanlı, rol bazlı (Bayi / Admin)
- Dosya yükleme: Dekont için görsel/PDF upload (max 10MB önerilir)
- Mail bildirimi: Her durum geçişinde tetiklenir, şablon sistemi üzerinden gönderilir (bkz. E-posta Bildirim Sistemi)
- Para birimi: TL (Türk Lirası), ileride çoklu para birimi desteği planlanabilir
- Stok düşümü: Sadece admin dekontu onayladığında tetiklenmeli (Aşama 5)
