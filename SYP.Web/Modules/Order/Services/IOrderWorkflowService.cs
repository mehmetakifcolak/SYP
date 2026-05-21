using System.Collections.Generic;

namespace SYP.Order;

public interface IOrderWorkflowService
{
    /// <summary>
    /// Belirtilen eski durum ve rol için geçerli hedef durumları döner.
    /// </summary>
    IReadOnlyList<OrderStatus> GetAllowedTransitions(OrderStatus currentStatus, string userRole);

    /// <summary>
    /// Geçiş kuralını doğrular; geçersizse ValidationError fırlatır.
    /// </summary>
    void ValidateTransition(OrderStatus oldStatus, OrderStatus newStatus, string userRole);

    /// <summary>
    /// Bu duruma geçişte kullanılacak e-posta şablon anahtarı.
    /// </summary>
    string GetEmailTemplateKey(OrderStatus newStatus);

    /// <summary>
    /// Bu duruma geçişte mail alacak taraf: "Bayi" veya "Temsilci".
    /// </summary>
    string GetEmailRecipientRole(OrderStatus newStatus);

    /// <summary>
    /// Durum terminalse (artık değiştirilemezse) true döner.
    /// </summary>
    bool IsTerminalStatus(OrderStatus status);

    /// <summary>
    /// Red/iptal nedeni zorunlu olan durumlar için true döner.
    /// </summary>
    bool RequiresReason(OrderStatus newStatus);
}
