using MyRow = SYP.Setting.NumberTemplatesRow;

namespace SYP.Setting;

public interface INumberTemplatesDeleteHandler : IDeleteHandler<MyRow, DeleteRequest, DeleteResponse> { }

public class NumberTemplatesDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<MyRow, DeleteRequest, DeleteResponse>(context),
    INumberTemplatesDeleteHandler
{
}