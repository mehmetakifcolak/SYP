using Microsoft.Extensions.Caching.Memory;

namespace SYP.Administration;

public class ImpersonateTokenService(IMemoryCache cache)
{
    public string GenerateToken(int userId)
    {
        var token = Guid.NewGuid().ToString("N");
        cache.Set("imptk_" + token, userId, TimeSpan.FromMinutes(2));
        return token;
    }

    public int? ConsumeToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return null;
        var key = "imptk_" + token;
        if (cache.TryGetValue(key, out int userId))
        {
            cache.Remove(key);
            return userId;
        }
        return null;
    }
}
