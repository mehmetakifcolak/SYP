using Serenity.Services;

namespace SYP.Administration;

public interface IAuditLogListHandler : IListHandler<AuditLogRow, ListRequest, ListResponse<AuditLogRow>> { }

public class AuditLogListHandler(IRequestContext context) :
    ListRequestHandler<AuditLogRow, ListRequest, ListResponse<AuditLogRow>>(context),
    IAuditLogListHandler
{
}
