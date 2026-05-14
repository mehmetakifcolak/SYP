using MyRow = SYP.Customer.CustomersRow;

namespace SYP.Customer;

public interface ICustomersListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class CustomersListHandler(IRequestContext context) :
    ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>(context),
    ICustomersListHandler
{
}