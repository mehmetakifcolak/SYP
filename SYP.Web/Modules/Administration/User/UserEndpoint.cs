using Serenity.Reporting;
using SYP.Email;
using SYP.Email.Services;
using System.Globalization;
using MyRow = SYP.Administration.UserRow;

namespace SYP.Administration.Endpoints;

public class GetImpersonateTokenRequest : ServiceRequest
{
    public int UserId { get; set; }
}

public class GetImpersonateTokenResponse : ServiceResponse
{
    public string Token { get; set; }
}

public class SendUserCredentialsRequest : ServiceRequest
{
    public int UserId { get; set; }
}

public class SendUserCredentialsResponse : ServiceResponse
{
    public string Email { get; set; }
}

[Route("Services/Administration/User/[action]")]
[ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
public class UserEndpoint : ServiceEndpoint
{
    [HttpPost, AuthorizeCreate(typeof(MyRow))]
    public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request, [FromServices] IUserSaveHandler handler)
    {
        return handler.Create(uow, request);
    }

    [HttpPost, AuthorizeUpdate(typeof(MyRow))]
    public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request, [FromServices] IUserSaveHandler handler)
    {
        return handler.Update(uow, request);
    }

    [HttpPost, AuthorizeDelete(typeof(MyRow))]
    public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request, [FromServices] IUserDeleteHandler handler)
    {
        return handler.Delete(uow, request);
    }

    public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request, [FromServices] IUserRetrieveHandler handler)
    {
        return handler.Retrieve(connection, request);
    }

    public ListResponse<MyRow> List(IDbConnection connection, UserListRequest request, [FromServices] IUserListHandler handler)
    {
        return handler.List(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(MyRow))]
    public FileContentResult ListExcel(IDbConnection connection, UserListRequest request,
        [FromServices] IUserListHandler handler,
        [FromServices] IExcelExporter exporter)
    {
        var data = List(connection, request, handler).Entities;
        var bytes = exporter.Export(data, typeof(Columns.UserColumns), request.ExportColumns);
        return ExcelContentResult.Create(bytes, "Users_" +
            DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
    }

    [HttpPost]
    public GetImpersonateTokenResponse GetImpersonateToken(
        [FromServices] ImpersonateTokenService tokenService,
        GetImpersonateTokenRequest request)
    {
        if (request?.UserId <= 0)
            throw new ArgumentNullException(nameof(request.UserId));

        return new GetImpersonateTokenResponse
        {
            Token = tokenService.GenerateToken(request.UserId)
        };
    }

    /// <summary>
    /// Seçilen kullanıcı için yeni bir geçici şifre üretir, hesabı aktifleştirir ve
    /// kullanıcı adı + geçici şifreyi e-posta kuyruğu üzerinden kullanıcıya gönderir.
    /// </summary>
    [HttpPost, AuthorizeUpdate(typeof(MyRow))]
    public SendUserCredentialsResponse SendCredentialsEmail(IUnitOfWork uow,
        SendUserCredentialsRequest request,
        [FromServices] ITwoLevelCache cache,
        [FromServices] IEmailQueueSender emailSender,
        [FromServices] IOptions<EnvironmentSettings> environmentOptions)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.UserId <= 0)
            throw new ArgumentNullException(nameof(request.UserId));

        var fld = MyRow.Fields;
        var user = uow.Connection.TryFirst<MyRow>(q => q
            .Select(fld.UserId, fld.Username, fld.DisplayName, fld.Email)
            .Where(fld.UserId == request.UserId));

        if (user == null)
            throw new ValidationError("Kullanıcı bulunamadı.");

        if (string.IsNullOrWhiteSpace(user.Email))
            throw new ValidationError("Bu kullanıcının tanımlı bir e-posta adresi yok. " +
                "Lütfen önce kullanıcıya e-posta adresi ekleyin.");

        // Geçici şifre üret ve kaydet
        var tempPassword = UserHelper.GenerateTemporaryPassword();
        string salt = null;
        var hash = UserHelper.GenerateHash(tempPassword, ref salt);

        uow.Connection.UpdateById(new MyRow
        {
            UserId = user.UserId.Value,
            PasswordHash = hash,
            PasswordSalt = salt,
            IsActive = 1,
            LastDirectoryUpdate = DateTime.Now
        });

        // Şifre değiştiği için önbellekteki kullanıcı tanımını geçersiz kıl
        cache.InvalidateOnCommit(uow, fld);

        var externalUrl = environmentOptions?.Value?.SiteExternalUrl.TrimToNull() ??
            Request.GetBaseUri().ToString();
        var loginLink = UriHelper.Combine(externalUrl, "Account/Login");

        // E-postayı kuyruğa ekle (varsayılan SMTP / şablon yoksa hata fırlatır,
        // bu durumda yukarıdaki şifre güncellemesi de geri alınır)
        emailSender.QueueTemplateEmailAsync(new QueueTemplateEmailRequest
        {
            TemplateKey = "MAIL_KULLANICI_GIRIS_BILGILERI",
            To = new List<string> { user.Email },
            TemplateData = new Dictionary<string, object>
            {
                { "ad_soyad", user.DisplayName },
                { "kullanici_adi", user.Username },
                { "sifre", tempPassword },
                { "giris_link", loginLink }
            },
            Priority = EmailPriority.High,
            ReferenceType = "User",
            ReferenceId = user.UserId?.ToString()
        }).GetAwaiter().GetResult();

        return new SendUserCredentialsResponse
        {
            Email = user.Email
        };
    }
}