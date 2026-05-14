using MyRow = SYP.Setting.BankAccountInformationsRow;

namespace SYP.Setting;

public interface IBankAccountInformationsRetrieveHandler : IRetrieveHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>> { }

public class BankAccountInformationsRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>>(context),
    IBankAccountInformationsRetrieveHandler
{
}