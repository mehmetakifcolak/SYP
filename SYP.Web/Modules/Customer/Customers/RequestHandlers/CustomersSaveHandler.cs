using Serenity;
using Serenity.Data;
using SYP.Setting;
using SYP.Administration;
using SYP.Email.Services;
using Microsoft.Extensions.Configuration;
using MyRow = SYP.Customer.CustomersRow;

namespace SYP.Customer;

public interface ICustomersSaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class CustomersSaveHandler : SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>, ICustomersSaveHandler
{
    private readonly IEmailQueueSender _emailQueueSender;
    private readonly IConfiguration _configuration;
    private string? _plainPassword; // Şifreyi hash'lemeden önce sakla

    public CustomersSaveHandler(IRequestContext context, IEmailQueueSender emailQueueSender, IConfiguration configuration)
        : base(context)
    {
        _emailQueueSender = emailQueueSender;
        _configuration = configuration;
    }

    protected override void BeforeSave()
    {
        base.BeforeSave();

        // Yeni kayıt ve Code boşsa otomatik numara oluştur
        if (IsCreate && Request.Entity.Code.IsNullOrEmpty())
        {
            // NumberTemplates'ten Müşteri tipi için template al
            var template = Connection.TryFirst<NumberTemplatesRow>(q => q.SelectTableFields()
                .Where(NumberTemplatesRow.Fields.Type == (int)global::_Ext.NumberTemplateType.Musteri &
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
                throw new ValidationError("Numara şablonu bulunamadı! Lütfen önce 'Müşteri' tipinde bir numara şablonu tanımlayın.");
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

        // Email zorunluluk kontrolü (User oluşturulacaksa)
        if (!Row.Password.IsNullOrEmpty() && Row.Email.IsNullOrEmpty())
        {
            throw new ValidationError("Kullanıcı oluşturmak için email adresi zorunludur!");
        }

        // Password validation (User oluşturulacaksa)
        if (!Row.Password.IsNullOrEmpty())
        {
            ValidatePasswordForUserCreation();
            _plainPassword = Row.Password; // Mail için şifreyi sakla
        }
    }

    protected override void AfterSave()
    {
        base.AfterSave();

        // Eğer password girilmişse ve UserId yoksa User oluştur
        if (!Row.Password.IsNullOrEmpty() && Row.UserId == null)
        {
            CreateUserForCustomer();

            // Hoşgeldin emaili gönder
            SendWelcomeEmail();
        }
    }

    private void SendWelcomeEmail()
    {
        if (Row.Email.IsNullOrEmpty() || _plainPassword.IsNullOrEmpty())
            return;

        try
        {
            var siteUrl = _configuration["EnvironmentSettings:SiteExternalUrl"] ?? "http://localhost";
            var companyName = "SYP"; // veya configuration'dan alınabilir

            _ = _emailQueueSender.QueueTemplateEmailAsync(new QueueTemplateEmailRequest
            {
                TemplateKey = "NEW_CUSTOMER_WELCOME",
                To = new List<string> { Row.Email },
                TemplateData = new Dictionary<string, object?>
                {
                    { "CustomerName", Row.Name },
                    { "Username", Row.Email },
                    { "Password", _plainPassword },
                    { "CompanyName", companyName },
                    { "LoginUrl", siteUrl + "/Account/Login" },
                    { "Year", DateTime.Now.Year.ToString() }
                },
                ReferenceType = "Customer",
                ReferenceId = Row.Id?.ToString()
            });
        }
        catch
        {
            // Email gönderimi başarısız olsa bile işlem devam etsin
        }
    }

    private void ValidatePasswordForUserCreation()
    {
        // Password minimum uzunluk kontrolü
        if (Row.Password.Length < 6)
            throw new ValidationError("Şifre en az 6 karakter olmalıdır!");

        // Password eşleşme kontrolü
        if (Row.Password != Row.PasswordConfirm)
            throw new ValidationError("Şifreler eşleşmiyor!");

        // Email (Username) benzersizlik kontrolü
        var existingUser = Connection.TryFirst<UserRow>(q => q
            .Select(UserRow.Fields.UserId)
            .Where(UserRow.Fields.Username == Row.Email));

        if (existingUser != null)
            throw new ValidationError($"'{Row.Email}' email adresi ile zaten bir kullanıcı mevcut!");
    }

    private void CreateUserForCustomer()
    {
        // Password hash oluştur
        string salt = null;
        var passwordHash = UserHelper.GenerateHash(Row.Password, ref salt);

        // User Row oluştur
        var userRow = new UserRow
        {
            Username = Row.Email,
            DisplayName = Row.Name,
            Email = Row.Email,
            PasswordHash = passwordHash,
            PasswordSalt = salt,
            Source = "site",
            IsActive = 1,
            InsertDate = DateTime.Now,
            InsertUserId = Convert.ToInt32(Context.User.GetIdentifier())
        };

        // User'ı kaydet
        var userId = Connection.InsertAndGetID(userRow);

        // Customer'a UserId'yi ata
        Connection.UpdateById(new MyRow
        {
            Id = Row.Id,
            UserId = (int)userId
        });

        // Row'a UserId'yi ekle (UI'da göstermek için)
        Row.UserId = (int)userId;
    }
}