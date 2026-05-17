using Microsoft.AspNetCore.Mvc;
using Serenity.Reporting;
using Serenity.Services;
using SYP.Setting;
using System.Data;
using System.Globalization;

namespace SYP.Warehouse.Endpoints;

[Route("Services/Warehouse/StockExits/[action]")]
[ConnectionKey(typeof(StockExitsRow)), ServiceAuthorize(typeof(StockExitsRow))]
public class StockExitsEndpoint : ServiceEndpoint
{
    [HttpPost, AuthorizeCreate(typeof(StockExitsRow))]
    public SaveResponse Create(IUnitOfWork uow, SaveRequest<StockExitsRow> request,
        [FromServices] IStockExitsSaveHandler handler)
    {
        return handler.Create(uow, request);
    }

    [HttpPost, AuthorizeUpdate(typeof(StockExitsRow))]
    public SaveResponse Update(IUnitOfWork uow, SaveRequest<StockExitsRow> request,
        [FromServices] IStockExitsSaveHandler handler)
    {
        return handler.Update(uow, request);
    }

    [HttpPost, AuthorizeDelete(typeof(StockExitsRow))]
    public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
        [FromServices] IStockExitsDeleteHandler handler)
    {
        return handler.Delete(uow, request);
    }

    [HttpPost]
    public RetrieveResponse<StockExitsRow> Retrieve(IDbConnection connection, RetrieveRequest request,
        [FromServices] IStockExitsRetrieveHandler handler)
    {
        return handler.Retrieve(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(StockExitsRow))]
    public ListResponse<StockExitsRow> List(IDbConnection connection, ListRequest request,
        [FromServices] IStockExitsListHandler handler)
    {
        return handler.List(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(StockExitsRow))]
    public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
        [FromServices] IStockExitsListHandler handler,
        [FromServices] IExcelExporter exporter)
    {
        var data = List(connection, request, handler).Entities;
        var bytes = exporter.Export(data, typeof(Columns.StockExitsColumns), request.ExportColumns);
        return ExcelContentResult.Create(bytes, "StokCikislari_" +
            DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
    }

    [HttpPost, AuthorizeCreate(typeof(StockExitsRow))]
    public GetNextNumberResponse GetNextExitNo(IDbConnection connection)
    {
        var template = connection.TryFirst<NumberTemplatesRow>(q => q.SelectTableFields()
            .Where(NumberTemplatesRow.Fields.Type == (int)NumberTemplateType.StokCikisi &
                   NumberTemplatesRow.Fields.Active == 1));

        string prefix;
        int length;

        if (template != null)
        {
            prefix = template.Prefix ?? "SCK";

            if (!template.DateFormat.IsEmptyOrNull())
            {
                prefix = prefix + DateTime.Now.ToString(template.DateFormat);
                if (!template.Suffix.IsEmptyOrNull())
                    prefix = prefix + template.Suffix;
            }

            length = prefix.Length + (template.Length ?? 5);
        }
        else
        {
            prefix = "SCK" + DateTime.Now.ToString("yyyyMM");
            length = prefix.Length + 5;
        }

        var request = new GetNextNumberRequest
        {
            Length = length,
            Prefix = prefix
        };

        return GetNextNumberHelper.GetNextNumber(
            connection,
            request,
            StockExitsRow.Fields.ExitNo,
            StockExitsRow.Fields.Id
        );
    }
}
