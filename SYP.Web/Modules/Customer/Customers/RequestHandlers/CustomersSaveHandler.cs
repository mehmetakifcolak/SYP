using MyRow = SYP.Customer.CustomersRow;

namespace SYP.Customer;

public interface ICustomersSaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class CustomersSaveHandler(IRequestContext context) :
    SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>(context),
    ICustomersSaveHandler
{
}