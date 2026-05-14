using MyRow = SYP.Setting.BankAccountInformationsRow;

namespace SYP.Setting;

public interface IBankAccountInformationsSaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class BankAccountInformationsSaveHandler(IRequestContext context) :
    SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>(context),
    IBankAccountInformationsSaveHandler
{
}