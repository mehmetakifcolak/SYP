using Serenity;
using Serenity.Data;
using SYP.Setting;
using MyRow = SYP.Catalog.PriceListsRow;

namespace SYP.Catalog;

public interface IPriceListsSaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class PriceListsSaveHandler(IRequestContext context) :
    SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>(context),
    IPriceListsSaveHandler
{
    protected override void BeforeSave()
    {
        base.BeforeSave();

        // Yeni kayıt ve Code boşsa otomatik numara oluştur
        if (IsCreate && Request.Entity.Code.IsNullOrEmpty())
        {
            // NumberTemplates'ten Fiyat Listesi tipi için template al
            var template = Connection.TryFirst<NumberTemplatesRow>(q => q.SelectTableFields()
                .Where(NumberTemplatesRow.Fields.Type == (int)NumberTemplateType.FiyatListesi &
                       NumberTemplatesRow.Fields.Active == 1));

            if (template != null)
            {
                var prefix = template.Prefix ?? "";

                // Eğer tarih formatı varsa prefix'e ekle
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

                Row.Code = GetNextNumberHelper.GetNextNumber(
                    Connection,
                    request,
                    MyRow.Fields.Code,
                    MyRow.Fields.Id
                ).Serial;
            }
            else
            {
                throw new ValidationError("Numara şablonu bulunamadı! Lütfen önce 'Fiyat Listesi' tipinde bir numara şablonu tanımlayın.");
            }
        }

        // Code alanının unique olduğunu kontrol et
        if (!Row.Code.IsNullOrEmpty())
        {
            var existingRecord = Connection.TryFirst<MyRow>(q => q
                .Select(MyRow.Fields.Code)
                .Select(MyRow.Fields.Id)
                .Where(MyRow.Fields.Code == Row.Code));

            if (IsCreate && existingRecord != null)
            {
                throw new ValidationError($"'{existingRecord.Code}' kodu ile kayıt zaten mevcut!");
            }

            if (IsUpdate && existingRecord != null && existingRecord.Id != Row.Id)
            {
                throw new ValidationError($"'{existingRecord.Code}' kodu ile başka bir kayıt mevcut!");
            }
        }
    }

    protected override void ValidateRequest()
    {
        base.ValidateRequest();

        var row = Row;

        // ValidTo >= ValidFrom kontrolü
        if (row.ValidFrom.HasValue && row.ValidTo.HasValue && row.ValidTo < row.ValidFrom)
        {
            throw new ValidationError("ValidTo", "Geçerlilik bitiş tarihi, başlangıç tarihinden küçük olamaz!");
        }

        // IsDefault uniqueness kontrolü (aynı para biriminde)
        if (row.IsDefault == true)
        {
            var fld = MyRow.Fields;
            var allLists = Connection.List<MyRow>(q => q
                .Select(fld.Id, fld.IsDefault)
                .Where(fld.CurrencyId == row.CurrencyId.Value));

            var hasDefault = allLists.Any(x => x.IsDefault == true && x.Id != (Row.Id ?? 0));

            if (hasDefault)
            {
                throw new ValidationError("IsDefault",
                    "Bu para birimi için zaten varsayılan bir fiyat listesi mevcut!");
            }
        }
    }

    protected override void AfterSave()
    {
        base.AfterSave();

        // Master-detail kaydetme - PriceListItems
        if (Row.ItemList != null)
        {
            var fld = PriceListItemsRow.Fields;

            // Mevcut kalemleri getir
            var oldList = new HashSet<int>(
                Connection.List<PriceListItemsRow>(
                    new Criteria(fld.PriceListId) == Row.Id.Value)
                .Select(x => x.Id.Value)
            );

            foreach (var item in Row.ItemList)
            {
                item.PriceListId = Row.Id.Value;

                if (item.Id == null || item.Id.Value <= 0)
                {
                    // Yeni kayıt ekle
                    new PriceListItemsSaveHandler(Context).Create(UnitOfWork,
                        new SaveRequest<PriceListItemsRow> { Entity = item });
                }
                else
                {
                    // Mevcut kaydı güncelle
                    oldList.Remove(item.Id.Value);
                    new PriceListItemsSaveHandler(Context).Update(UnitOfWork,
                        new SaveRequest<PriceListItemsRow> { Entity = item });
                }
            }

            // Silinmiş kalemleri kaldır
            foreach (var id in oldList)
            {
                new PriceListItemsDeleteHandler(Context).Delete(UnitOfWork,
                    new DeleteRequest { EntityId = id });
            }
        }
    }
}