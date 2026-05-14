using Serenity;
using Serenity.Data;
using SYP.Setting;
using MyRow = SYP.Catalog.ProductsRow;

namespace SYP.Catalog;

public interface IProductsSaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class ProductsSaveHandler(IRequestContext context) :
    SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>(context),
    IProductsSaveHandler
{
    protected override void BeforeSave()
    {
        base.BeforeSave();

        // Yeni kayıt ve Code boşsa otomatik numara oluştur
        if (IsCreate && Request.Entity.Code.IsNullOrEmpty())
        {
            // NumberTemplates'ten Ürün tipi için template al
            var template = Connection.TryFirst<NumberTemplatesRow>(q => q.SelectTableFields()
                .Where(NumberTemplatesRow.Fields.Type == (int)global::_Ext.NumberTemplateType.Urun &
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
                throw new ValidationError("Numara şablonu bulunamadı! Lütfen önce 'Ürün' tipinde bir numara şablonu tanımlayın.");
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
}