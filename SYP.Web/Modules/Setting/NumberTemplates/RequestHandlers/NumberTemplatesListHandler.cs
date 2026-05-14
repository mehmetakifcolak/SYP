using MyRow = SYP.Setting.NumberTemplatesRow;

namespace SYP.Setting;

public interface INumberTemplatesListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class NumberTemplatesListHandler(IRequestContext context) :
    ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>(context),
    INumberTemplatesListHandler
{
}