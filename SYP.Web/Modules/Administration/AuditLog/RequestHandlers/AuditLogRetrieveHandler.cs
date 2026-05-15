using Serenity.Services;

namespace SYP.Administration;

public interface IAuditLogRetrieveHandler : IRetrieveHandler<AuditLogRow, RetrieveRequest, RetrieveResponse<AuditLogRow>> { }

public class AuditLogRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<AuditLogRow, RetrieveRequest, RetrieveResponse<AuditLogRow>>(context),
    IAuditLogRetrieveHandler
{
}
