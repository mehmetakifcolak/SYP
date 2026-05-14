using MyRow = SYP.Customer.CustomersRow;

namespace SYP.Customer;

public interface ICustomersDeleteHandler : IDeleteHandler<MyRow, DeleteRequest, DeleteResponse> { }

public class CustomersDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<MyRow, DeleteRequest, DeleteResponse>(context),
    ICustomersDeleteHandler
{
}