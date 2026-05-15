using Serenity.Reporting;
using System.Data;
using System.Globalization;
using SYP.Administration;
using MyRow = SYP.Customer.CustomersRow;

namespace SYP.Customer.Endpoints;

[Route("Services/Customer/Customers/[action]")]
[ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
public class CustomersEndpoint : ServiceEndpoint
{
    [HttpPost, AuthorizeCreate(typeof(MyRow))]
    public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
        [FromServices] ICustomersSaveHandler handler)
    {
        return handler.Create(uow, request);
    }

    [HttpPost, AuthorizeUpdate(typeof(MyRow))]
    public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
        [FromServices] ICustomersSaveHandler handler)
    {
        return handler.Update(uow, request);
    }
 
    [HttpPost, AuthorizeDelete(typeof(MyRow))]
    public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
        [FromServices] ICustomersDeleteHandler handler)
    {
        return handler.Delete(uow, request);
    }

    [HttpPost, AuthorizeRetrieve(typeof(MyRow))]
    public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
        [FromServices] ICustomersRetrieveHandler handler)
    {
        return handler.Retrieve(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(MyRow))]
    public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
        [FromServices] ICustomersListHandler handler)
    {
        return handler.List(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(MyRow))]
    public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
        [FromServices] ICustomersListHandler handler,
        [FromServices] IExcelExporter exporter)
    {
        var data = List(connection, request, handler).Entities;
        var bytes = exporter.Export(data, typeof(Columns.CustomersColumns), request.ExportColumns);
        return ExcelContentResult.Create(bytes, "CustomersList_" +
            DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
    }

    [HttpPost, AuthorizeUpdate(typeof(MyRow))]
    public CreateUserResponse CreateUser(IUnitOfWork uow, CreateUserRequest request,
        [FromServices] ITwoLevelCache cache)
    {
        if (request.CustomerId <= 0)
            throw new ArgumentException("CustomerId gereklidir!");

        // Customer'ı getir
        var customer = uow.Connection.TryFirst<MyRow>(q => q
            .SelectTableFields()
            .Where(MyRow.Fields.Id == request.CustomerId));

        if (customer == null)
            throw new ValidationError("Müşteri bulunamadı!");

        if (customer.UserId.HasValue)
            throw new ValidationError("Bu müşteri için zaten bir kullanıcı mevcut!");

        if (customer.Email.IsNullOrEmpty())
            throw new ValidationError("Kullanıcı oluşturmak için müşteri email adresi zorunludur!");

        // Password validation
        if (request.Password.IsNullOrEmpty())
            throw new ValidationError("Şifre zorunludur!");

        if (request.Password.Length < 6)
            throw new ValidationError("Şifre en az 6 karakter olmalıdır!");

        if (request.Password != request.PasswordConfirm)
            throw new ValidationError("Şifreler eşleşmiyor!");

        // Username (Email) benzersizlik kontrolü
        var existingUser = uow.Connection.TryFirst<UserRow>(q => q
            .Select(UserRow.Fields.UserId)
            .Where(UserRow.Fields.Username == customer.Email));

        if (existingUser != null)
            throw new ValidationError($"'{customer.Email}' email adresi ile zaten bir kullanıcı mevcut!");

        // Password hash oluştur
        string salt = null;
        var passwordHash = UserHelper.GenerateHash(request.Password, ref salt);

        // Mevcut kullanici ID'sini al
        int? currentUserId = null;
        var identifier = User.GetIdentifier();
        if (int.TryParse(identifier, out var parsedId))
        {
            currentUserId = parsedId;
        }
        else if (!string.IsNullOrEmpty(identifier))
        {
            var currentUser = uow.Connection.TryFirst<UserRow>(q => q
                .Select(UserRow.Fields.UserId)
                .Where(UserRow.Fields.Username == identifier));
            currentUserId = currentUser?.UserId;
        }

        // User oluştur
        var userRow = new UserRow
        {
            Username = customer.Email,
            DisplayName = customer.Name,
            Email = customer.Email,
            PasswordHash = passwordHash,
            PasswordSalt = salt,
            Source = "site",
            IsActive = 1,
            InsertDate = DateTime.Now,
            InsertUserId = currentUserId
        };

        var userId = (int)uow.Connection.InsertAndGetID(userRow);

        // Customer'a UserId ata
        uow.Connection.UpdateById(new MyRow
        {
            Id = request.CustomerId,
            UserId = userId
        });

        // Cache'i invalidate et
        cache.InvalidateOnCommit(uow, UserRow.Fields);

        return new CreateUserResponse
        {
            UserId = userId,
            Username = customer.Email
        };
    }
}