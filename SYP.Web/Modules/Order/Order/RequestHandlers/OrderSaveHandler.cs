using Serenity;
using Serenity.Data;
using Serenity.Services;
using SYP.Email.Services;
using SYP.Setting;
using SYP.Customer.Services;
using SYP.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using MyRow = SYP.Order.OrderRow;

namespace SYP.Order;

public interface IOrderSaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class OrderSaveHandler : SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>, IOrderSaveHandler
{
    private readonly IOrderStatusService _statusService;
    private readonly IEmailQueueSender _emailSender;
    private readonly IGetBayiiCustomerService _bayiiCustomerService;

    public OrderSaveHandler(IRequestContext context, IOrderStatusService statusService, IEmailQueueSender emailSender, IGetBayiiCustomerService bayiiCustomerService)
        : base(context)
    {
        _statusService = statusService;
        _emailSender = emailSender;
        _bayiiCustomerService = bayiiCustomerService;
    }

    protected override void BeforeSave()
    {
        base.BeforeSave();

        // 0. IsCreate ve CustomerId boş ise permission kontrolü yap
        if (IsCreate && !Row.CustomerId.HasValue)
        {
            // Bayi permission'a sahipse CustomerId'yi otomatik doldur
            if (Permissions.HasPermission(Administration.PermissionKeys.Bayii))
            {
                var bayiiCustomerId = _bayiiCustomerService.GetCurrentBayiiCustomerId();
                if (bayiiCustomerId.HasValue)
                {
                    Row.CustomerId = bayiiCustomerId;
                }
            }
        }
        // 1. Auto-numbering
        if (IsCreate && Row.OrderNumber.IsNullOrEmpty())
        {
            Row.OrderNumber = GenerateOrderNumber();
        }

        // 2. Varsayılan değerler
        if (IsCreate)
        {
            if (Row.OrderDate == null)
                Row.OrderDate = DateTime.Now;

            if (Row.Status == null)
                Row.Status = OrderStatus.TALEP_GONDERILDI;

            // Audit alanlarını doldur
            Row.InsertDate = DateTime.Now;
            Row.InsertUserId = int.Parse(Context.User.GetIdentifier());
        }

        // Update için audit alanlarını doldur
        if (IsUpdate)
        {
            Row.UpdateDate = DateTime.Now;
            Row.UpdateUserId = int.Parse(Context.User.GetIdentifier());
        }

        // 3. Bayi'nin yöneticisini otomatik ata
        if (IsCreate && Row.CustomerId.HasValue && !Row.ManagerUserId.HasValue)
        {
            var customer = Connection.TryById<Customer.CustomersRow>(Row.CustomerId.Value);
            if (customer != null)
            {
                Row.ManagerUserId = customer.ManagerUserId;
            }
        }

        // 4. Detaylardan tutarları hesapla
        if (Row.DetailList != null && Row.DetailList.Count > 0)
        {
            CalculateTotals();
        }

        // 5. Durum değişikliği validasyonu
        if (IsUpdate && Old.Status != Row.Status)
        {
            ValidateStatusTransition();
        }
    }

    protected override void AfterSave()
    {
        base.AfterSave();

        // 6. Durum geçişi logu
        if (IsUpdate && Old.Status != Row.Status)
        {
            var userId = int.Parse(Context.User.GetIdentifier());
            var userRole = GetUserRole(userId);

            _statusService.LogStatusChange(
                Connection,
                Row.Id.Value,
                Old.Status,
                Row.Status,
                userId,
                userRole,
                Row.RejectReason
            );

            // 7. Email bildirimi gönder
            SendStatusChangeEmail(Old.Status.Value, Row.Status.Value);
        }

        // 8. Stok güncelleme (HAZIRLANIYOR durumuna geçerken)
        if (IsUpdate && Row.Status == OrderStatus.HAZIRLANIYOR && Old.Status != OrderStatus.HAZIRLANIYOR)
        {
            UpdateStockFromOrder();
        }
    }

    private string GenerateOrderNumber()
    {
        var template = Connection.TryFirst<NumberTemplatesRow>(q => q.SelectTableFields()
            .Where(NumberTemplatesRow.Fields.Type == (int)NumberTemplateType.B2BSiparis &
                   NumberTemplatesRow.Fields.Active == 1));

        string prefix = "B2B";
        int length = 10;

        if (template != null)
        {
            prefix = template.Prefix ?? "B2B";
            if (!template.DateFormat.IsNullOrEmpty())
            {
                prefix += DateTime.Now.ToString(template.DateFormat);
                if (!template.Suffix.IsNullOrEmpty())
                    prefix += template.Suffix;
            }
            length = prefix.Length + (template.Length ?? 5);
        }
        else
        {
            prefix = "B2B" + DateTime.Now.ToString("yyyyMM");
            length = prefix.Length + 5;
        }

        return GetNextNumberHelper.GetNextNumber(
            Connection,
            new GetNextNumberRequest { Length = length, Prefix = prefix },
            OrderRow.Fields.OrderNumber,
            OrderRow.Fields.Id
        ).Serial;
    }

    private void CalculateTotals()
    {
        // Satır toplamlarını hesapla
        foreach (var detail in Row.DetailList)
        {
            detail.LineTotal = (detail.Quantity ?? 0) * (detail.UnitPrice ?? 0) - (detail.Discount ?? 0);
        }

        // Toplam tutar
        Row.TotalAmount = Row.DetailList.Sum(d => d.LineTotal ?? 0);

        // Kademeli indirim hesapla
        var tieredDiscount = CalculateTieredDiscount(Row.TotalAmount.Value);
        Row.DiscountPercentage = tieredDiscount.percentage;
        Row.DiscountAmount = tieredDiscount.amount;

        // Net tutar
        Row.NetAmount = Row.TotalAmount - Row.DiscountAmount;
    }

    private (decimal percentage, decimal amount) CalculateTieredDiscount(decimal totalAmount)
    {
        var tdsFields = TieredDiscountSettingsRow.Fields;
        var settings = Connection.List<TieredDiscountSettingsRow>(q => q
            .SelectTableFields()
            .Where(new Criteria(tdsFields.IsActive) > 0)
            .OrderBy(tdsFields.MinAmount, desc: true));

        foreach (var setting in settings)
        {
            if (totalAmount >= setting.MinAmount)
            {
                var discountAmount = totalAmount * (setting.DiscountPercentage.Value / 100);
                return (setting.DiscountPercentage.Value, discountAmount);
            }
        }

        return (0, 0);
    }

    private void ValidateStatusTransition()
    {
        var oldStatus = Old.Status.Value;
        var newStatus = Row.Status.Value;

        // Terminal durumlardan çıkış kontrolü
        if (oldStatus == OrderStatus.TESLIM_EDILDI || oldStatus == OrderStatus.TALEP_IPTAL)
        {
            throw new ValidationError("Tamamlanan veya iptal edilen siparişler güncellenemez!");
        }

        // Red durumları için açıklama zorunlu
        if (newStatus == OrderStatus.BAYI_REDDETTI ||
            newStatus == OrderStatus.DEKONT_REDDEDILDI ||
            newStatus == OrderStatus.TALEP_IPTAL)
        {
            if (Row.RejectReason.IsNullOrEmpty())
            {
                throw new ValidationError("Red/iptal nedeni zorunludur!");
            }
        }

        // Rol bazlı geçiş kontrolü
        var userId = int.Parse(Context.User.GetIdentifier());
        var userRole = GetUserRole(userId);

        switch (userRole)
        {
            case "Bayi":
                ValidateDealerStatusTransition(oldStatus, newStatus);
                break;
            case "Yönetici":
                ValidateManagerStatusTransition(oldStatus, newStatus);
                break;
            case "SüperAdmin":
                // Süper admin her geçişi yapabilir
                break;
            default:
                throw new ValidationError("Bu işlem için yetkiniz yok!");
        }
    }

    private void ValidateDealerStatusTransition(OrderStatus oldStatus, OrderStatus newStatus)
    {
        var validTransitions = new Dictionary<OrderStatus, List<OrderStatus>>
        {
            { OrderStatus.REVIZE_EDILDI, new List<OrderStatus> { OrderStatus.BAYI_ONAYLADI, OrderStatus.BAYI_REDDETTI } },
            { OrderStatus.BAYI_ONAYLADI, new List<OrderStatus> { OrderStatus.DEKONT_YUKLENDI } },
            { OrderStatus.DEKONT_REDDEDILDI, new List<OrderStatus> { OrderStatus.DEKONT_YUKLENDI, OrderStatus.TALEP_IPTAL } },
        };

        if (!validTransitions.ContainsKey(oldStatus) || !validTransitions[oldStatus].Contains(newStatus))
        {
            throw new ValidationError($"Geçersiz durum geçişi: {oldStatus} → {newStatus}");
        }
    }

    private void ValidateManagerStatusTransition(OrderStatus oldStatus, OrderStatus newStatus)
    {
        var validTransitions = new Dictionary<OrderStatus, List<OrderStatus>>
        {
            { OrderStatus.TALEP_GONDERILDI, new List<OrderStatus> { OrderStatus.REVIZE_EDILDI, OrderStatus.TALEP_IPTAL } },
            { OrderStatus.BAYI_REDDETTI, new List<OrderStatus> { OrderStatus.REVIZE_EDILDI, OrderStatus.TALEP_IPTAL } },
            { OrderStatus.DEKONT_YUKLENDI, new List<OrderStatus> { OrderStatus.DEKONT_REDDEDILDI, OrderStatus.HAZIRLANIYOR } },
            { OrderStatus.HAZIRLANIYOR, new List<OrderStatus> { OrderStatus.SEVK_ASAMASINDA } },
            { OrderStatus.SEVK_ASAMASINDA, new List<OrderStatus> { OrderStatus.TESLIM_EDILDI } },
        };

        if (!validTransitions.ContainsKey(oldStatus) || !validTransitions[oldStatus].Contains(newStatus))
        {
            throw new ValidationError($"Geçersiz durum geçişi: {oldStatus} → {newStatus}");
        }
    }

    private void UpdateStockFromOrder()
    {
        var detailFields = OrderDetailRow.Fields;
        var details = Connection.List<OrderDetailRow>(q => q
            .SelectTableFields()
            .Where(new Criteria(detailFields.OrderId) == Row.Id.Value));

        // Varsayılan depo (ilk aktif depo)
        var warehouseId = GetDefaultWarehouseId();

        foreach (var detail in details)
        {
            var stockFields = Warehouse.WarehouseStockRow.Fields;
            var existingStock = Connection.TryFirst<Warehouse.WarehouseStockRow>(q => q
                .SelectTableFields()
                .Where(
                    new Criteria(stockFields.WarehouseId) == warehouseId &
                    new Criteria(stockFields.ProductId) == detail.ProductId.Value));

            if (existingStock != null)
            {
                // Stok miktarını düş
                var newQuantity = (existingStock.Quantity ?? 0) - (detail.Quantity ?? 0);

                if (newQuantity < 0)
                {
                    throw new ValidationError($"Ürün '{detail.ProductCodeName}' için yeterli stok yok! Mevcut: {existingStock.Quantity}, Talep: {detail.Quantity}");
                }

                Connection.UpdateById(new Warehouse.WarehouseStockRow
                {
                    Id = existingStock.Id,
                    Quantity = newQuantity,
                    LastUpdateDate = DateTime.Now
                });
            }
            else
            {
                throw new ValidationError($"Ürün '{detail.ProductCodeName}' için stok kaydı bulunamadı!");
            }
        }
    }

    private int GetDefaultWarehouseId()
    {
        var whFields = Warehouse.WarehousesRow.Fields;
        var warehouse = Connection.TryFirst<Warehouse.WarehousesRow>(q => q
            .Select(whFields.Id)
            .Where(new Criteria(whFields.IsActive) > 0)
            .OrderBy(whFields.Id));

        if (warehouse == null)
            throw new ValidationError("Sistemde aktif depo bulunamadı!");

        return warehouse.Id.Value;
    }

    private void SendStatusChangeEmail(OrderStatus oldStatus, OrderStatus newStatus)
    {
        var templateKey = GetEmailTemplateKey(newStatus);
        var recipients = GetEmailRecipients(newStatus);

        if (recipients.Count == 0 || templateKey.IsNullOrEmpty())
            return;

        var customer = Connection.TryById<Customer.CustomersRow>(Row.CustomerId.Value);
        var siteUrl = "https://localhost:5001"; // TODO: Site URL'ini appsettings'ten al
        var userId = int.Parse(Context.User.GetIdentifier());
        var user = Connection.TryById<Administration.UserRow>(userId);

        _ = _emailSender.QueueTemplateEmailAsync(new QueueTemplateEmailRequest
        {
            TemplateKey = templateKey,
            To = recipients,
            TemplateData = new Dictionary<string, object>
            {
                { "siparis_no", Row.OrderNumber },
                { "bayi_adi", customer?.Name },
                { "toplam_tutar", Row.NetAmount?.ToString("N2") + " " + Row.CurrencyCode },
                { "durum", newStatus.ToString() },
                { "aciklama", Row.RejectReason ?? Row.Notes },
                { "siparis_link", $"{siteUrl}/Order/Order#{Row.Id}" },
                { "degistiren_kullanici", user?.DisplayName }
            },
            ReferenceType = "Order",
            ReferenceId = Row.Id?.ToString()
        });
    }

    private string GetEmailTemplateKey(OrderStatus status)
    {
        return status switch
        {
            OrderStatus.TALEP_GONDERILDI => "MAIL_YENI_TALEP",
            OrderStatus.REVIZE_EDILDI => "MAIL_REVIZE_EDILDI",
            OrderStatus.BAYI_ONAYLADI => "MAIL_BAYI_ONAYLADI",
            OrderStatus.BAYI_REDDETTI => "MAIL_BAYI_REDDETTI",
            OrderStatus.DEKONT_YUKLENDI => "MAIL_DEKONT_YUKLENDI",
            OrderStatus.DEKONT_REDDEDILDI => "MAIL_DEKONT_REDDEDILDI",
            OrderStatus.HAZIRLANIYOR => "MAIL_HAZIRLANIYOR",
            OrderStatus.SEVK_ASAMASINDA => "MAIL_SEVK_ASAMASINDA",
            OrderStatus.TESLIM_EDILDI => "MAIL_TESLIM_EDILDI",
            OrderStatus.TALEP_IPTAL => "MAIL_TALEP_IPTAL",
            _ => null
        };
    }

    private List<string> GetEmailRecipients(OrderStatus status)
    {
        var recipients = new List<string>();
        var customer = Connection.TryById<Customer.CustomersRow>(Row.CustomerId.Value);
        var manager = Row.ManagerUserId.HasValue ? Connection.TryById<Administration.UserRow>(Row.ManagerUserId.Value) : null;

        // Bayi aksiyonları → Yönetici'ye
        if (status == OrderStatus.TALEP_GONDERILDI ||
            status == OrderStatus.BAYI_ONAYLADI ||
            status == OrderStatus.BAYI_REDDETTI ||
            status == OrderStatus.DEKONT_YUKLENDI)
        {
            if (manager?.Email != null)
                recipients.Add(manager.Email);
        }
        // Yönetici/Admin aksiyonları → Bayi'ye
        else
        {
            if (customer?.Email != null)
                recipients.Add(customer.Email);
        }

        return recipients;
    }

    private string GetUserRole(int userId)
    {
        // Süper Admin kontrolü
        if (Context.User.IsInRole("Admin"))
            return "SüperAdmin";

        // Yönetici kontrolü
        var isManager = Connection.Exists<Customer.CustomersRow>(
            new Criteria(Customer.CustomersRow.Fields.ManagerUserId) == userId);

        if (isManager)
            return "Yönetici";

        // Bayi kontrolü
        var isDealer = Connection.Exists<Customer.CustomersRow>(
            new Criteria(Customer.CustomersRow.Fields.UserId) == userId);

        if (isDealer)
            return "Bayi";

        throw new ValidationError("Kullanıcı rolü belirlenemedi!");
    }
}
