// AuditLog artik AuditLogBehavior ile calisir.
// IAuditedRow interface'ini Row'lara ekleyerek otomatik loglama aktif edilir.
//
// Ornek:
// public sealed class CustomersRow : Row<CustomersRow.RowFields>, IIdRow, INameRow, IAuditedRow
// {
//     ...
// }

namespace SYP.Administration;

public interface IAuditLogService
{
}

public class AuditLogService : IAuditLogService
{
}
