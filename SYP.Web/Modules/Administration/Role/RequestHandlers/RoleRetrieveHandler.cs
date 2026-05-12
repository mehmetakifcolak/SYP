using MyRow = SYP.Administration.RoleRow;

namespace SYP.Administration;

public interface IRoleRetrieveHandler : IRetrieveHandler<MyRow> { }
public class RoleRetrieveHandler(IRequestContext context)
    : RetrieveRequestHandler<MyRow>(context), IRoleRetrieveHandler
{
}