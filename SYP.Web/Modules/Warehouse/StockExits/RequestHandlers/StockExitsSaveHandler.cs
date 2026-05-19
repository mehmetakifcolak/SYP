using Serenity.Data;
using Serenity.Services;
using SYP.Setting;
using MyRow = SYP.Warehouse.StockExitsRow;

namespace SYP.Warehouse;

public interface IStockExitsSaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class StockExitsSaveHandler : SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>, IStockExitsSaveHandler
{
    private StockExitStatus? _oldStatus;

    public StockExitsSaveHandler(IRequestContext context)
        : base(context)
    {
    }

    protected override void BeforeSave()
    {
        base.BeforeSave();

        // Eski durumu kaydet (update için)
        if (IsUpdate)
        {
            var existing = Connection.TryById<MyRow>(Row.Id.Value);
            _oldStatus = existing?.Status;
        }

        // Yeni kayıt ve ExitNo boşsa otomatik numara oluştur
        if (IsCreate && Row.ExitNo.IsNullOrEmpty())
        {
            var template = Connection.TryFirst<NumberTemplatesRow>(q => q.SelectTableFields()
                .Where(NumberTemplatesRow.Fields.Type == (int)NumberTemplateType.StokCikisi &
                       NumberTemplatesRow.Fields.Active == 1));

            if (template != null)
            {
                var prefix = template.Prefix ?? "SCK";

                if (!template.DateFormat.IsEmptyOrNull())
                {
                    prefix = prefix + DateTime.Now.ToString(template.DateFormat);
                    if (!template.Suffix.IsEmptyOrNull())
                        prefix = prefix + template.Suffix;
                }

                // SCK prefix'i kullan (Stok Çıkış)
                prefix = "SCK" + DateTime.Now.ToString("yyyyMM");

                var request = new GetNextNumberRequest
                {
                    Length = prefix.Length + 5,
                    Prefix = prefix
                };

                Row.ExitNo = GetNextNumberHelper.GetNextNumber(
                    Connection,
                    request,
                    MyRow.Fields.ExitNo,
                    MyRow.Fields.Id
                ).Serial;
            }
            else
            {
                // Varsayılan numara formatı
                var prefix = "SCK" + DateTime.Now.ToString("yyyyMM");
                var request = new GetNextNumberRequest
                {
                    Length = prefix.Length + 5,
                    Prefix = prefix
                };

                Row.ExitNo = GetNextNumberHelper.GetNextNumber(
                    Connection,
                    request,
                    MyRow.Fields.ExitNo,
                    MyRow.Fields.Id
                ).Serial;
            }
        }

        // Varsayılan tarih
        if (IsCreate && Row.ExitDate == null)
        {
            Row.ExitDate = DateTime.Now;
        }

        // Onaylama işleminde stok kontrolü yap
        bool willApprove = false;
        if (IsCreate && Row.Status == StockExitStatus.Approved)
        {
            willApprove = true;
        }
        else if (IsUpdate && Row.Status == StockExitStatus.Approved && _oldStatus != StockExitStatus.Approved)
        {
            willApprove = true;
        }

        if (willApprove)
        {
            ValidateStockAvailability();
        }
    }

    private void ValidateStockAvailability()
    {
        // Detayları al
        var details = Row.DetailList;
        if (details == null || details.Count == 0)
        {
            throw new ValidationError("Stok çıkışı onaylanmadan önce en az bir ürün eklemelisiniz!");
        }

        var stockFields = WarehouseStockRow.Fields;
        var insufficientProducts = new List<string>();

        foreach (var detail in details)
        {
            if (detail.ProductId == null || detail.Quantity == null || detail.Quantity <= 0)
                continue;

            // Mevcut stok kaydını bul
            var existingStock = Connection.TryFirst<WarehouseStockRow>(q => q
                .SelectTableFields()
                .Select(stockFields.ProductCode)
                .Select(stockFields.ProductName)
                .Where(
                    new Criteria(stockFields.WarehouseId) == Row.WarehouseId.Value &
                    new Criteria(stockFields.ProductId) == detail.ProductId.Value));

            var availableQty = existingStock?.Quantity ?? 0;
            var requestedQty = detail.Quantity ?? 0;

            if (availableQty < requestedQty)
            {
                var productName = existingStock?.ProductName ?? detail.ProductName ?? $"Ürün #{detail.ProductId}";
                insufficientProducts.Add($"{productName} (Mevcut: {availableQty:N4}, İstenen: {requestedQty:N4})");
            }
        }

        if (insufficientProducts.Count > 0)
        {
            throw new ValidationError($"Yetersiz stok! Aşağıdaki ürünler için yeterli stok bulunmamaktadır:\n" +
                string.Join("\n", insufficientProducts));
        }
    }

    protected override void AfterSave()
    {
        base.AfterSave();

        // Detail'leri join ile tekrar yükle
        ReloadDetailsWithJoins();

        // Sadece durumu yeni "Onaylandı" yapıldığında stok güncelle
        bool shouldUpdateStock = false;

        if (IsCreate && Row.Status == StockExitStatus.Approved)
        {
            shouldUpdateStock = true;
        }
        else if (IsUpdate && Row.Status == StockExitStatus.Approved && _oldStatus != StockExitStatus.Approved)
        {
            shouldUpdateStock = true;
        }

        if (shouldUpdateStock)
        {
            UpdateWarehouseStock();
        }
    }

    private void ReloadDetailsWithJoins()
    {
        // Detail'leri join ile tekrar yükle
        var detailFields = StockExitDetailsRow.Fields;
        var details = Connection.List<StockExitDetailsRow>(q => q
            .SelectTableFields()
            .Select(detailFields.ProductCode)
            .Select(detailFields.ProductName)
            .Where(new Criteria(detailFields.StockExitId) == Row.Id.Value));

        // Response'a ekle
        Row.DetailList = details;
    }

    private void UpdateWarehouseStock()
    {
        // Detayları al
        var detailFields = StockExitDetailsRow.Fields;
        var details = Connection.List<StockExitDetailsRow>(q => q
            .SelectTableFields()
            .Where(new Criteria(detailFields.StockExitId) == Row.Id.Value));

        foreach (var detail in details)
        {
            var stockFields = WarehouseStockRow.Fields;

            // Mevcut stok kaydını bul
            var existingStock = Connection.TryFirst<WarehouseStockRow>(q => q
                .SelectTableFields()
                .Where(
                    new Criteria(stockFields.WarehouseId) == Row.WarehouseId.Value &
                    new Criteria(stockFields.ProductId) == detail.ProductId.Value));

            if (existingStock != null)
            {
                // Miktarı DÜŞÜR (çıkış olduğu için)
                var newQuantity = (existingStock.Quantity ?? 0) - (detail.Quantity ?? 0);

                Connection.UpdateById(new WarehouseStockRow
                {
                    Id = existingStock.Id,
                    Quantity = newQuantity,
                    LastUpdateDate = DateTime.Now
                });
            }
            // Eğer stok kaydı yoksa, zaten yetersiz stok hatası alınmış olmalı
            // Ama güvenlik için yine de oluşturabiliriz (negatif olacak ama loglama için)
        }
    }
}
