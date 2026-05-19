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

    public GetBayiiCustomerService(IRequestContext context, ISqlConnections sqlConnections)
    {
        _context = context;
        _sqlConnections = sqlConnections;
    }

    /// <summary>
    /// Mevcut kullanıcının Bayii customer ID'sini getirir.
    /// Eğer kullanıcı bir customer ile eşleşmiyorsa null döner.
    /// </summary>
    public int? GetCurrentBayiiCustomerId()
    {
        try
        {
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
