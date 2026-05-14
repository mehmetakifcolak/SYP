using MyRow = SYP.Setting.NumberTemplatesRow;

namespace SYP.Setting;

public interface INumberTemplatesRetrieveHandler : IRetrieveHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>> { }

public class NumberTemplatesRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>>(context),
    INumberTemplatesRetrieveHandler
{
}