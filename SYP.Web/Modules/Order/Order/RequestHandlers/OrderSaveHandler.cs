using Microsoft.Extensions.Logging;
using Serenity;
using Serenity.Data;
using Serenity.Services;
using SYP.Email.Services;
using SYP.Setting;
using SYP.Customer.Services;
using SYP.Administration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using MyRow = SYP.Order.OrderRow;

namespace SYP.Order;

public interface IOrderSaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class OrderSaveHandler : SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>, IOrderSaveHandler
{
    private readonly IOrderStatusService _statusService;
    private readonly IOrderWorkflowService _workflow;
    private readonly IEmailQueueSender _emailSender;
    private readonly IGetBayiiCustomerService _bayiiCustomerService;
    private readonly ILogger<OrderSaveHandler> _logger;

    public OrderSaveHandler(IRequestContext context, IOrderStatusService statusService,
        IOrderWorkflowService workflow, IEmailQueueSender emailSender,
        IGetBayiiCustomerService bayiiCustomerService, ILogger<OrderSaveHandler> logger)
        : base(context)
    {
        _statusService = statusService;
        _workflow = workflow;
        _emailSender = emailSender;
        _bayiiCustomerService = bayiiCustomerService;
        _logger = logger;
    }

    protected override void BeforeSave()
    {
        base.BeforeSave();

        if (IsCreate && !Row.CustomerId.HasValue)
        {
            if (Permissions.HasPermission(Administration.PermissionKeys.Bayii))
            {
                var bayiiCustomerId = _bayiiCustomerService.GetCurrentBayiiCustomerId();
                if (bayiiCustomerId.HasValue)
                    Row.CustomerId = bayiiCustomerId;
                else
                    throw new ValidationError("Bayii bilgisi bulunamadı!");
            }
            else
            {
                throw new ValidationError("Lütfen bir bayi seçiniz!");
            }
        }

        if (IsCreate && Row.OrderNumber.IsNullOrEmpty())
            Row.OrderNumber = GenerateOrderNumber();

        if (IsCreate)
        {
            Row.OrderDate   ??= DateTime.Now;
            Row.Status      ??= OrderStatus.TALEP_BEKLETTE;
            Row.InsertDate    = DateTime.Now;
            Row.InsertUserId  = int.Parse(Context.User.GetIdentifier());
        }

        if (IsUpdate)
        {
            Row.UpdateDate   = DateTime.Now;
            Row.UpdateUserId = int.Parse(Context.User.GetIdentifier());
        }

        if (IsCreate && Row.CustomerId.HasValue && !Row.ManagerUserId.HasValue)
        {
            var customer = Connection.TryById<Customer.CustomersRow>(Row.CustomerId.Value);
            if (customer != null)
                Row.ManagerUserId = customer.ManagerUserId;
        }

        if (Row.DetailList != null && Row.DetailList.Count > 0)
            CalculateTotals();

        if (IsUpdate && Old.Status != Row.Status)
            ValidateStatusTransition();
    }

    protected override void AfterSave()
    {
        base.AfterSave();

        if (IsUpdate && Old.Status != Row.Status)
        {
            var userId   = int.Parse(Context.User.GetIdentifier());
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

            SendStatusChangeEmail(Row.Status.Value);
        }

        if (IsUpdate && Row.Status == OrderStatus.HAZIRLANIYOR && Old.Status != OrderStatus.HAZIRLANIYOR)
            ValidateStockAvailability();

        if (IsUpdate && Row.Status == OrderStatus.SEVK_ASAMASINDA && Old.Status != OrderStatus.SEVK_ASAMASINDA
            && !(Old.IsStockExitCreated ?? false))
            CreateStockExitFromOrder();
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
        foreach (var detail in Row.DetailList)
            detail.LineTotal = (detail.Quantity ?? 0) * (detail.UnitPrice ?? 0) - (detail.Discount ?? 0);

        Row.TotalAmount = Row.DetailList.Sum(d => d.LineTotal ?? 0);

        var tieredDiscount = CalculateTieredDiscount(Row.TotalAmount.Value);
        Row.DiscountPercentage = tieredDiscount.percentage;
        Row.DiscountAmount     = tieredDiscount.amount;
        Row.NetAmount          = Row.TotalAmount - Row.DiscountAmount;
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
            if (setting.MinAmount.HasValue && setting.DiscountPercentage.HasValue
                && totalAmount >= setting.MinAmount.Value)
            {
                var pct = setting.DiscountPercentage.Value;
                return (pct, totalAmount * (pct / 100));
            }
        }

        return (0, 0);
    }

    private void ValidateStatusTransition()
    {
        var oldStatus = Old.Status.Value;
        var newStatus = Row.Status.Value;
        var userId    = int.Parse(Context.User.GetIdentifier());
        var userRole  = GetUserRole(userId);

        if (_workflow.RequiresReason(newStatus) && Row.RejectReason.IsNullOrEmpty())
            throw new ValidationError("Bu durum için açıklama/neden girilmesi zorunludur!");

        _workflow.ValidateTransition(oldStatus, newStatus, userRole);
    }

    private void ValidateStockAvailability()
    {
        var detailFields = OrderDetailRow.Fields;
        var details = Connection.List<OrderDetailRow>(q => q
            .SelectTableFields()
            .Where(new Criteria(detailFields.OrderId) == Row.Id.Value));

        var warehouseId = Row.WarehouseId ?? GetDefaultWarehouseId();

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
                var availableQuantity = existingStock.Quantity ?? 0;
                var requestedQuantity = detail.Quantity ?? 0;

                if (availableQuantity < requestedQuantity)
                    throw new ValidationError($"Ürün '{detail.ProductCodeName}' için yeterli stok yok! Mevcut: {availableQuantity}, Talep: {requestedQuantity}");
            }
            else
            {
                throw new ValidationError($"Ürün '{detail.ProductCodeName}' için stok kaydı bulunamadı!");
            }
        }
    }

    private void CreateStockExitFromOrder()
    {
        var warehouseId = Row.WarehouseId ?? GetDefaultWarehouseId();

        var prefix = "SCK" + DateTime.Now.ToString("yyyyMM");
        var exitNo = GetNextNumberHelper.GetNextNumber(
            Connection,
            new GetNextNumberRequest { Length = prefix.Length + 5, Prefix = prefix },
            Warehouse.StockExitsRow.Fields.ExitNo,
            Warehouse.StockExitsRow.Fields.Id
        ).Serial;

        var userId = int.Parse(Context.User.GetIdentifier());

        var exitRow = new Warehouse.StockExitsRow
        {
            ExitNo      = exitNo,
            WarehouseId = warehouseId,
            ExitDate    = DateTime.Now,
            Description = $"Sipariş teslimi: {Row.OrderNumber}",
            Status      = Warehouse.StockExitStatus.Approved,
            InsertDate  = DateTime.Now,
            InsertUserId = userId
        };

        Connection.Insert(exitRow);
        var insertedExit = Connection.TryFirst<Warehouse.StockExitsRow>(q => q
            .Select(Warehouse.StockExitsRow.Fields.Id)
            .Where(Warehouse.StockExitsRow.Fields.ExitNo == exitNo));
        var exitId = insertedExit?.Id
            ?? throw new ValidationError("Stok çıkış kaydı oluşturulamadı.");

        var detailFields = OrderDetailRow.Fields;
        var details = Connection.List<OrderDetailRow>(q => q
            .SelectTableFields()
            .Where(new Criteria(detailFields.OrderId) == Row.Id.Value));

        if (details == null || details.Count == 0)
        {
            throw new ValidationError("Sipariş detayları bulunamadı. Stok çıkışı oluşturulamıyor.");
        }

        foreach (var detail in details)
        {
            Connection.Insert(new Warehouse.StockExitDetailsRow
            {
                StockExitId = exitId,
                ProductId   = detail.ProductId,
                Quantity    = detail.Quantity,
                UnitPrice   = detail.UnitPrice,
                VatRate     = detail.VatRate,
                Notes       = Row.OrderNumber
            });
        }

        // IsStockExitCreated read-only olduğu için SQL ile güncelliyoruz
        Connection.Execute(@"
            UPDATE Orders
            SET IsStockExitCreated = 1,
                UpdateDate = @UpdateDate,
                UpdateUserId = @UpdateUserId
            WHERE Id = @Id",
            new {
                Id = Row.Id.Value,
                UpdateDate = DateTime.Now,
                UpdateUserId = userId
            });
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

    /// <summary>
    /// Durum her değiştiğinde hem bayiye hem de sorumlu yöneticiye (temsilciye)
    /// AYRI AYRI, kendilerine hitap eden, sipariş kalemlerini miktarlarıyla içeren
    /// e-posta gönderir. E-posta hatası sipariş kaydını etkilemez.
    /// </summary>
    private void SendStatusChangeEmail(OrderStatus newStatus)
    {
        try
        {
            var customer = Row.CustomerId.HasValue
                ? Connection.TryById<Customer.CustomersRow>(Row.CustomerId.Value) : null;
            var manager = Row.ManagerUserId.HasValue
                ? Connection.TryById<Administration.UserRow>(Row.ManagerUserId.Value) : null;

            var bayiHasMail     = !customer?.Email.IsNullOrEmpty() ?? false;
            var temsilciHasMail = !manager?.Email.IsNullOrEmpty() ?? false;
            if (!bayiHasMail && !temsilciHasMail)
                return;

            var bayiAdi     = customer?.Name.TrimToNull() ?? Row.CustomerName ?? "Bayi";
            var temsilciAdi = manager?.DisplayName.TrimToNull() ?? Row.ManagerName ?? "Sorumlu Yönetici";

            var currency = Row.CurrencyId.HasValue
                ? Connection.TryById<Setting.CurrencyListRow>(Row.CurrencyId.Value) : null;
            var cur = currency?.Symbol.TrimToNull() ?? currency?.Code.TrimToNull() ?? Row.CurrencyCode ?? "";

            var tr    = CultureInfo.GetCultureInfo("tr-TR");
            var lines = BuildOrderLines(cur, tr);

            var statusLabel = newStatus.GetDescription();
            var accent      = AccentColorFor(newStatus);
            var orderNo     = Row.OrderNumber;
            var changedBy   = Connection.TryById<Administration.UserRow>(int.Parse(Context.User.GetIdentifier()))?.DisplayName;
            var reason      = Row.RejectReason.TrimToNull() ?? Row.Notes.TrimToNull();
            var discount    = (Row.DiscountAmount ?? 0) > 0
                ? (Row.DiscountAmount ?? 0).ToString("N2", tr) + " " + cur : null;
            var net         = (Row.NetAmount ?? Row.TotalAmount ?? 0).ToString("N2", tr) + " " + cur;

            const string siteUrl = "https://localhost:5001";
            var link    = $"{siteUrl}/Order/Order#{Row.Id}";
            var subject = OrderStatusEmailBuilder.BuildSubject(statusLabel, orderNo);

            OrderStatusEmailBuilder.Model Build(string recipientName, string introHtml) => new()
            {
                RecipientName = recipientName,
                IntroHtml     = introHtml,
                StatusLabel   = statusLabel,
                AccentColor   = accent,
                OrderNumber   = orderNo,
                BayiName      = bayiAdi,
                TemsilciName  = temsilciAdi,
                OrderDate     = (Row.OrderDate ?? DateTime.Now).ToString("dd.MM.yyyy", tr),
                Lines         = lines,
                DiscountTotal = discount,
                NetTotal      = net,
                Reason        = reason,
                ChangedBy     = changedBy,
                OrderLink     = link
            };

            if (bayiHasMail)
            {
                var intro = $"<strong>{Enc(orderNo)}</strong> numaralı siparişinizin durumu " +
                            $"<strong>{Enc(statusLabel)}</strong> olarak güncellendi. " +
                            "Sipariş kalemleri aşağıda yer almaktadır.";
                QueueOrderEmail(customer.Email, subject, Build(bayiAdi, intro));
            }

            if (temsilciHasMail)
            {
                var intro = $"<strong>{Enc(bayiAdi)}</strong> bayisine ait <strong>{Enc(orderNo)}</strong> numaralı " +
                            $"siparişin durumu <strong>{Enc(statusLabel)}</strong> olarak güncellendi. " +
                            "Sipariş kalemleri aşağıda yer almaktadır.";
                QueueOrderEmail(manager.Email, subject, Build(temsilciAdi, intro));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Sipariş durum e-postası gönderilemedi. OrderId: {OrderId}", Row?.Id);
        }
    }

    private List<OrderStatusEmailBuilder.Line> BuildOrderLines(string currency, CultureInfo tr)
    {
        var df = OrderDetailRow.Fields;
        var details = Connection.List<OrderDetailRow>(q => q
            .SelectTableFields()
            .Select(df.ProductCodeName)
            .Select(df.UnitCode)
            .Where(new Criteria(df.OrderId) == Row.Id.Value)
            .OrderBy(df.Id));

        var lines = new List<OrderStatusEmailBuilder.Line>();
        var no = 1;
        foreach (var d in details)
        {
            var qty = (d.Quantity ?? 0).ToString("0.####", tr);
            if (!d.UnitCode.IsNullOrEmpty())
                qty += " " + d.UnitCode;

            lines.Add(new OrderStatusEmailBuilder.Line
            {
                No        = no++,
                Product   = d.ProductCodeName.TrimToNull() ?? ("#" + d.ProductId),
                Quantity  = qty,
                UnitPrice = (d.UnitPrice ?? 0).ToString("N2", tr) + " " + currency,
                LineTotal = (d.LineTotal ?? 0).ToString("N2", tr) + " " + currency
            });
        }
        return lines;
    }

    private void QueueOrderEmail(string toEmail, string subject, OrderStatusEmailBuilder.Model model)
    {
        _ = _emailSender.QueueEmailAsync(new QueueEmailRequest
        {
            To            = new List<string> { toEmail },
            Subject       = subject,
            Body          = OrderStatusEmailBuilder.BuildBody(model),
            BodyText      = OrderStatusEmailBuilder.BuildPlainText(model),
            ReferenceType = "Order",
            ReferenceId   = Row.Id?.ToString()
        });
    }

    private static string Enc(string s) => WebUtility.HtmlEncode(s ?? "");

    private static string AccentColorFor(OrderStatus s) => s switch
    {
        OrderStatus.TESLIM_ALINDI or OrderStatus.DEKONT_ONAYLANDI
            or OrderStatus.TEMSILCI_ONAYLADI or OrderStatus.BAYI_ONAYLADI => "#16a34a",
        OrderStatus.BAYI_REDDETTI or OrderStatus.DEKONT_REDDEDILDI
            or OrderStatus.TESLIM_ALINMADI => "#dc2626",
        OrderStatus.TALEP_IPTAL => "#6b7280",
        OrderStatus.SEVK_ASAMASINDA or OrderStatus.KARGO_HAZIRLANIYOR
            or OrderStatus.HAZIRLANIYOR => "#0891b2",
        _ => "#7c3aed"
    };

    private string GetUserRole(int userId)
    {
        // Bayii permission'ı varsa ve güvenlik yetkisi yoksa → Bayi
        if (Permissions.HasPermission(Administration.PermissionKeys.Bayii)
            && !Permissions.HasPermission("Administration:Security"))
            return "Bayi";

        // DB: yönetici mi?
        if (Connection.Exists<Customer.CustomersRow>(
                new Criteria(Customer.CustomersRow.Fields.ManagerUserId) == userId))
            return "Yönetici";

        // DB: bayi kullanıcısı mı?
        if (Connection.Exists<Customer.CustomersRow>(
                new Criteria(Customer.CustomersRow.Fields.UserId) == userId))
            return "Bayi";

        // Fallback: oturumu açık, yetkili kullanıcı → Yönetici
        return "Yönetici";
    }
}
