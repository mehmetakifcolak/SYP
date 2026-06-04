using Serenity.Data;
using Serenity.Services;
using SYP.Setting;
using MyRow = SYP.Warehouse.StockEntriesRow;

namespace SYP.Warehouse;

public interface IStockEntriesSaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class StockEntriesSaveHandler : SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>, IStockEntriesSaveHandler
{
    public StockEntriesSaveHandler(IRequestContext context)
        : base(context)
    {
    }

    protected override void BeforeSave()
    {
        base.BeforeSave();

        // Yeni kayıt ve EntryNo boşsa otomatik numara oluştur
        if (IsCreate && Row.EntryNo.IsNullOrEmpty())
        {
            var template = Connection.TryFirst<NumberTemplatesRow>(q => q.SelectTableFields()
                .Where(NumberTemplatesRow.Fields.Type == (int)NumberTemplateType.StokGirisi &
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

        // Detail'leri join ile tekrar yükle (response için)
        ReloadDetailsWithJoins();

        // NOT: Stok durumu artık WarehouseStockView üzerinden onaylı hareketlerden
        // dinamik hesaplanıyor; ayrıca bir stok tablosu güncellemesine gerek yok.
    }

    private void ReloadDetailsWithJoins()
    {
        // Detail'leri join ile tekrar yükle
        var detailFields = StockEntryDetailsRow.Fields;
        var details = Connection.List<StockEntryDetailsRow>(q => q
            .SelectTableFields()
            .Select(detailFields.ProductCode)
            .Select(detailFields.ProductName)
            .Where(new Criteria(detailFields.StockEntryId) == Row.Id.Value));

        // Response'a ekle
        Row.DetailList = details;
    }
}
