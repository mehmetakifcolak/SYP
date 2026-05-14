using Serenity.Data;
using System.Data;
using System.Linq;

namespace SYP;

public static class GetNextNumberHelper
{
    public static GetNextNumberResponse GetNextNumber(IDbConnection connection, GetNextNumberRequest request,
        Field field, Field idField)
    {
        var prefix = request.Prefix ?? "";

        var query = $@"
            SELECT TOP 1 {field.Expression}
            FROM {field.Fields.TableName} AS T0
            WHERE {field.Expression} LIKE @prefix + '%'
              AND LEN({field.Expression}) = @length
            ORDER BY CAST(SUBSTRING({field.Expression}, {prefix.Length + 1}, LEN({field.Expression}) - {prefix.Length}) AS INT) DESC";

        // Fetch the maximum number matching the pattern
        var max = connection.Query<string>(query, new { prefix, length = request.Length }).FirstOrDefault();

        var response = new GetNextNumberResponse();

        long l;
        response.Number = max == null ||
            !long.TryParse(max.Substring(prefix.Length), out l) ? 1 : l + 1;

        response.Serial = prefix + response.Number.ToString()
            .PadLeft(request.Length - prefix.Length, '0');

        return response;
    }
}
