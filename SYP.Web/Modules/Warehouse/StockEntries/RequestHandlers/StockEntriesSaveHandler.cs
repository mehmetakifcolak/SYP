using Serenity.Data;
using Serenity.Services;
using SYP.Setting;
using _Ext;
using MyRow = SYP.Warehouse.StockEntriesRow;

namespace SYP.Warehouse;

public interface IStockEntriesSaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class StockEntriesSaveHandler : SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>, IStockEntriesSaveHandler
{
    private StockEntryStatus? _oldStatus;

    public StockEntriesSaveHandler(IRequestContext context)
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

        // Yeni kayıt ve EntryNo boşsa otomatik numara oluştur
        if (IsCreate && Row.EntryNo.IsNullOrEmpty())
        {
            var template = Connection.TryFirst<NumberTemplatesRow>(q => q.SelectTableFields()
                .Where(NumberTemplatesRow.Fields.Type == (int)NumberTemplateType.Irsaliye &
                       NumberTemplatesRow.Fields.Active == 1));

            if (template != null)
            {
                var prefix = template.Prefix ?? "SGR";

                if (!template.DateFormat.IsEmptyOrNull())
                {
                    prefix = prefix + DateTime.Now.ToString(template.DateFormat);
                    if (!template.Suffix.IsEmptyOrNull())
                        prefix = prefix + template.Suffix;
                }

                var request = new GetNextNumberRequest
                {
                    Length = prefix.Length + (template.Length ?? 5),
                    Prefix = prefix
                };

                Row.EntryNo = GetNextNumberHelper.GetNextNumber(
                    Connection,
                    request,
                    MyRow.Fields.EntryNo,
                    MyRow.Fields.Id
                ).Serial;
            }
            else
            {
                // Varsayılan numara formatı
                var prefix = "SGR" + DateTime.Now.ToString("yyyyMM");
                var request = new GetNextNumberRequest
                {
                    Length = prefix.Length + 5,
                    Prefix = prefix
                };

                Row.EntryNo = GetNextNumberHelper.GetNextNumber(
                    Connection,
                    request,
                    MyRow.Fields.EntryNo,
                    MyRow.Fields.Id
                ).Serial;
            }
        }

        // Varsayılan tarih
        if (IsCreate && Row.EntryDate == null)
        {
            Row.EntryDate = DateTime.Now;
        }
    }

    protected override void AfterSave()
    {
        base.AfterSave();

        // Sadece durumu yeni "Onaylandı" yapıldığında stok güncelle
        // (İlk kez onaylandığında veya yeni kayıt onaylı oluşturulduğunda)
        bool shouldUpdateStock = false;

        if (IsCreate && Row.Status == StockEntryStatus.Approved)
        {
            // Yeni kayıt ve onaylı
            shouldUpdateStock = true;
        }
        else if (IsUpdate && Row.Status == StockEntryStatus.Approved && _oldStatus != StockEntryStatus.Approved)
        {
            // Mevcut kayıt, şimdi onaylandı (önceden onaylı değildi)
            shouldUpdateStock = true;
        }

        if (shouldUpdateStock)
        {
            UpdateWarehouseStock();
        }
    }

    private void UpdateWarehouseStock()
    {
        // Detayları al
        var detailFields = StockEntryDetailsRow.Fields;
        var details = Connection.List<StockEntryDetailsRow>(q => q
            .SelectTableFields()
            .Where(new Criteria(detailFields.StockEntryId) == Row.Id.Value));

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
                // Miktarı güncelle
                Connection.UpdateById(new WarehouseStockRow
                {
                    Id = existingStock.Id,
                    Quantity = (existingStock.Quantity ?? 0) + (detail.Quantity ?? 0),
                    LastUpdateDate = DateTime.Now
                });
            }
            else
            {
                // Yeni stok kaydı oluştur
                Connection.Insert(new WarehouseStockRow
                {
                    WarehouseId = Row.WarehouseId,
                    ProductId = detail.ProductId,
                    Quantity = detail.Quantity ?? 0,
                    LastUpdateDate = DateTime.Now
                });
            }
        }
    }
}
