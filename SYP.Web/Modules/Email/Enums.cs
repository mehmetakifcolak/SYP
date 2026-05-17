using Serenity.ComponentModel;
using System.ComponentModel;

namespace SYP.Email;

[EnumKey("Email.EmailQueueStatus"), ScriptInclude]
public enum EmailQueueStatus
{
    [Description("Beklemede")]
    Pending = 0,
    [Description("Zamanlanmış")]
    Scheduled = 1,
    [Description("İşleniyor")]
    Processing = 2,
    [Description("Gönderildi")]
    Sent = 3,
    [Description("Başarısız")]
    Failed = 4,
    [Description("İptal Edildi")]
    Cancelled = 5,
    [Description("Yeniden Deneniyor")]
    Retrying = 6
}

[EnumKey("Email.EmailPriority"), ScriptInclude]
public enum EmailPriority
{
    [Description("Yüksek")]
    High = 1,
    [Description("Normal")]
    Normal = 2,
    [Description("Düşük")]
    Low = 3
}

[EnumKey("Email.EmailLogStatus"), ScriptInclude]
public enum EmailLogStatus
{
    [Description("Kuyruğa Eklendi")]
    Queued = 0,
    [Description("İşleniyor")]
    Processing = 1,
    [Description("Gönderildi")]
    Sent = 2,
    [Description("Başarısız")]
    Failed = 3
}
