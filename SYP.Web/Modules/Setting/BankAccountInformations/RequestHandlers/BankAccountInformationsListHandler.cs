using MyRow = SYP.Setting.BankAccountInformationsRow;

namespace SYP.Setting;

public interface IBankAccountInformationsListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class BankAccountInformationsListHandler(IRequestContext context) :
    ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>(context),
    IBankAccountInformationsListHandler
{
}