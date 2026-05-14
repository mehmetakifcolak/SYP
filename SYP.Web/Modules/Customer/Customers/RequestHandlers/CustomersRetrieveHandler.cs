using MyRow = SYP.Customer.CustomersRow;

namespace SYP.Customer;

public interface ICustomersRetrieveHandler : IRetrieveHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>> { }

public class CustomersRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>>(context),
    ICustomersRetrieveHandler
{
}