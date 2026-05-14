using MyRow = SYP.Setting.NumberTemplatesRow;

namespace SYP.Setting;

public interface INumberTemplatesSaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class NumberTemplatesSaveHandler(IRequestContext context) :
    SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>(context),
    INumberTemplatesSaveHandler
{
}