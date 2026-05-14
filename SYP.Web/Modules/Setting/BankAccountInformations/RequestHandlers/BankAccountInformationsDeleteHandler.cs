using MyRow = SYP.Setting.BankAccountInformationsRow;

namespace SYP.Setting;

public interface IBankAccountInformationsDeleteHandler : IDeleteHandler<MyRow, DeleteRequest, DeleteResponse> { }

public class BankAccountInformationsDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<MyRow, DeleteRequest, DeleteResponse>(context),
    IBankAccountInformationsDeleteHandler
{
}