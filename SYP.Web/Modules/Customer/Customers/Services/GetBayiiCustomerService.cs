using Serenity;
using Serenity.Data;
using Serenity.Services;

namespace SYP.Customer.Services;

/// <summary>
/// Bayi permission'a sahip kullanıcılar için kendi CustomerId'sini sağlayan servis
/// </summary>
public interface IGetBayiiCustomerService
{
    int? GetCurrentBayiiCustomerId();
}

public class GetBayiiCustomerService : IGetBayiiCustomerService
{
    private readonly IRequestContext _context;
    private readonly ISqlConnections _sqlConnections;
    private readonly IPermissionService _permissions;

    public GetBayiiCustomerService(IRequestContext context, ISqlConnections sqlConnections, IPermissionService permissions)
    {
        _context = context;
        _sqlConnections = sqlConnections;
        _permissions = permissions;
    }

    /// <summary>
    /// Mevcut kullanıcının Bayii customer ID'sini getirir.
    /// Eğer kullanıcı bir customer ile eşleşmiyorsa veya admin yetkisine sahipse null döner.
    /// </summary>
    public int? GetCurrentBayiiCustomerId()
    {
        try
        {
            // Admin/Security yetkisi olanlar için null döndür (admin dashboard görsün)
            if (_permissions.HasPermission(Administration.PermissionKeys.Security))
                return null;

            // Temsilci yetkisi olanlar için de null döndür (admin dashboard görsün)
            if (_permissions.HasPermission(Administration.PermissionKeys.Temsilci))
                return null;

            // Bayii permission'ı yoksa null döndür
            if (!_permissions.HasPermission(Administration.PermissionKeys.Bayii))
                return null;

            var userId = int.Parse(_context.User.GetIdentifier());

            // Kullanıcının customer ID'sini bul
            using var connection = _sqlConnections.NewFor<CustomersRow>();
            var customerId = connection.TryFirst<CustomersRow>(q => q
                .Select(CustomersRow.Fields.Id)
                .Where(CustomersRow.Fields.UserId == userId))?.Id;

            return customerId;
        }
        catch
        {
            return null;
        }
    }
}
