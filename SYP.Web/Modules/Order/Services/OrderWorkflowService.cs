using Serenity.Services;
using System.Collections.Generic;
using System.Linq;

namespace SYP.Order;

/// <summary>
/// Sipariş durum akışı:
///
///  TALEP_GONDERILDI ──► [Temsilci] TEMSILCI_ONAYLADI ──► [Bayi] DEKONT_YUKLENDI
///       │                                 ▲                          │
///       ├──► [Temsilci] REVIZE_EDILDI ────┤          ┌──────────────┤
///       │         │                       │          ▼              │
///       │         ├──► [Bayi] BAYI_ONAYLADI ─────► TEMSILCI_ONAYLADI
///       │         └──► [Bayi] BAYI_REDDETTI ──► [Temsilci] REVIZE_EDILDI veya TALEP_IPTAL
///       └──► [Temsilci] TALEP_IPTAL
///
///  DEKONT_YUKLENDI ──► [Temsilci] DEKONT_ONAYLANDI ──► HAZIRLANIYOR ──► SEVK_ASAMASINDA
///                 └──► [Temsilci] DEKONT_REDDEDILDI ──► [Bayi] DEKONT_YUKLENDI veya TALEP_IPTAL
///
///  SEVK_ASAMASINDA ──► [Bayi] TESLIM_ALINDI (terminal)
///                 └──► [Bayi] TESLIM_ALINMADI (terminal)
/// </summary>
public class OrderWorkflowService : IOrderWorkflowService
{
    private static readonly Dictionary<(OrderStatus from, string role), List<OrderStatus>> Transitions = new()
    {
        // Temsilci geçişleri
        { (OrderStatus.TALEP_GONDERILDI, "Yönetici"),  [OrderStatus.TEMSILCI_ONAYLADI, OrderStatus.REVIZE_EDILDI, OrderStatus.TALEP_IPTAL] },
        { (OrderStatus.BAYI_ONAYLADI,    "Yönetici"),  [OrderStatus.TEMSILCI_ONAYLADI] },
        { (OrderStatus.BAYI_REDDETTI,    "Yönetici"),  [OrderStatus.REVIZE_EDILDI, OrderStatus.TALEP_IPTAL] },
        { (OrderStatus.DEKONT_YUKLENDI,  "Yönetici"),  [OrderStatus.DEKONT_ONAYLANDI, OrderStatus.DEKONT_REDDEDILDI] },
        { (OrderStatus.DEKONT_ONAYLANDI, "Yönetici"),  [OrderStatus.HAZIRLANIYOR] },
        { (OrderStatus.HAZIRLANIYOR,     "Yönetici"),  [OrderStatus.SEVK_ASAMASINDA] },

        // Talep beklemede geçişleri
        { (OrderStatus.TALEP_BEKLETTE,  "Yönetici"),  [OrderStatus.TALEP_GONDERILDI, OrderStatus.TALEP_IPTAL] },
        { (OrderStatus.TALEP_BEKLETTE,  "Bayi"),      [OrderStatus.TALEP_GONDERILDI, OrderStatus.TALEP_IPTAL] },

        // Bayi geçişleri
        { (OrderStatus.REVIZE_EDILDI,     "Bayi"),     [OrderStatus.BAYI_ONAYLADI, OrderStatus.BAYI_REDDETTI] },
        { (OrderStatus.TEMSILCI_ONAYLADI, "Bayi"),     [OrderStatus.DEKONT_YUKLENDI] },
        { (OrderStatus.DEKONT_REDDEDILDI, "Bayi"),     [OrderStatus.DEKONT_YUKLENDI, OrderStatus.TALEP_IPTAL] },
        { (OrderStatus.SEVK_ASAMASINDA,   "Bayi"),     [OrderStatus.TESLIM_ALINDI, OrderStatus.TESLIM_ALINMADI] },
    };

    private static readonly HashSet<OrderStatus> TerminalStatuses =
    [
        OrderStatus.TESLIM_ALINDI,
        OrderStatus.TESLIM_ALINMADI,
        OrderStatus.TALEP_IPTAL
    ];

    private static readonly HashSet<OrderStatus> ReasonRequired =
    [
        OrderStatus.BAYI_REDDETTI,
        OrderStatus.DEKONT_REDDEDILDI,
        OrderStatus.TESLIM_ALINMADI,
        OrderStatus.TALEP_IPTAL
    ];

    private static readonly Dictionary<OrderStatus, string> EmailTemplates = new()
    {
        { OrderStatus.TALEP_GONDERILDI,   "MAIL_YENI_TALEP" },
        { OrderStatus.REVIZE_EDILDI,      "MAIL_REVIZE_EDILDI" },
        { OrderStatus.BAYI_ONAYLADI,      "MAIL_BAYI_ONAYLADI" },
        { OrderStatus.BAYI_REDDETTI,      "MAIL_BAYI_REDDETTI" },
        { OrderStatus.TEMSILCI_ONAYLADI,  "MAIL_TEMSILCI_ONAYLADI" },
        { OrderStatus.DEKONT_YUKLENDI,    "MAIL_DEKONT_YUKLENDI" },
        { OrderStatus.DEKONT_ONAYLANDI,   "MAIL_DEKONT_ONAYLANDI" },
        { OrderStatus.DEKONT_REDDEDILDI,  "MAIL_DEKONT_REDDEDILDI" },
        { OrderStatus.HAZIRLANIYOR,       "MAIL_HAZIRLANIYOR" },
        { OrderStatus.SEVK_ASAMASINDA,    "MAIL_SEVK_ASAMASINDA" },
        { OrderStatus.TESLIM_ALINDI,      "MAIL_TESLIM_ALINDI" },
        { OrderStatus.TESLIM_ALINMADI,    "MAIL_TESLIM_ALINMADI" },
        { OrderStatus.TALEP_IPTAL,        "MAIL_TALEP_IPTAL" },
    };

    // Bayi aksiyonu → Temsilci'ye mail; Temsilci aksiyonu → Bayi'ye mail
    private static readonly HashSet<OrderStatus> BayiActions =
    [
        OrderStatus.TALEP_GONDERILDI,
        OrderStatus.BAYI_ONAYLADI,
        OrderStatus.BAYI_REDDETTI,
        OrderStatus.DEKONT_YUKLENDI,
        OrderStatus.TESLIM_ALINDI,
        OrderStatus.TESLIM_ALINMADI
    ];

    // Admin ve Temsilci tüm geçişleri yapabilir
    private static bool IsPrivilegedRole(string userRole) =>
        userRole is "SüperAdmin" or "Yönetici";

    public IReadOnlyList<OrderStatus> GetAllowedTransitions(OrderStatus currentStatus, string userRole)
    {
        if (IsPrivilegedRole(userRole))
        {
            // Mevcut durum dışında tüm durumlar geçilebilir
            return Enum.GetValues<OrderStatus>()
                .Where(s => s != currentStatus)
                .ToList();
        }

        var key = (currentStatus, userRole);
        return Transitions.TryGetValue(key, out var list) ? list : [];
    }

    public void ValidateTransition(OrderStatus oldStatus, OrderStatus newStatus, string userRole)
    {
        // Privileged kullanıcılar her geçişi yapabilir (terminal dahil)
        if (IsPrivilegedRole(userRole))
            return;

        if (IsTerminalStatus(oldStatus))
            throw new ValidationError("Tamamlanan veya iptal edilen sipariş durumu değiştirilemez!");

        var allowed = GetAllowedTransitions(oldStatus, userRole);
        if (!allowed.Contains(newStatus))
            throw new ValidationError($"Bu işlem için yetkiniz yok veya geçersiz durum geçişi: {oldStatus} → {newStatus}");
    }

    public string GetEmailTemplateKey(OrderStatus newStatus) =>
        EmailTemplates.TryGetValue(newStatus, out var key) ? key : null;

    public string GetEmailRecipientRole(OrderStatus newStatus) =>
        BayiActions.Contains(newStatus) ? "Yönetici" : "Bayi";

    public bool IsTerminalStatus(OrderStatus status) =>
        TerminalStatuses.Contains(status);

    public bool RequiresReason(OrderStatus newStatus) =>
        ReasonRequired.Contains(newStatus);
}
