using Microsoft.AspNetCore.Mvc;
using Serenity.Reporting;
using Serenity.Services;
using SYP.Setting;
using System.Data;
using System.Globalization;
using _Ext;

namespace SYP.Warehouse.Endpoints;

[Route("Services/Warehouse/StockEntries/[action]")]
[ConnectionKey(typeof(StockEntriesRow)), ServiceAuthorize(typeof(StockEntriesRow))]
public class StockEntriesEndpoint : ServiceEndpoint
{
    [HttpPost, AuthorizeCreate(typeof(StockEntriesRow))]
    public SaveResponse Create(IUnitOfWork uow, SaveRequest<StockEntriesRow> request,
        [FromServices] IStockEntriesSaveHandler handler)
    {
        return handler.Create(uow, request);
    }

    [HttpPost, AuthorizeUpdate(typeof(StockEntriesRow))]
    public SaveResponse Update(IUnitOfWork uow, SaveRequest<StockEntriesRow> request,
        [FromServices] IStockEntriesSaveHandler handler)
    {
        return handler.Update(uow, request);
    }

    [HttpPost, AuthorizeDelete(typeof(StockEntriesRow))]
    public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
        [FromServices] IStockEntriesDeleteHandler handler)
    {
        return handler.Delete(uow, request);
    }

    [HttpPost]
    public RetrieveResponse<StockEntriesRow> Retrieve(IDbConnection connection, RetrieveRequest request,
        [FromServices] IStockEntriesRetrieveHandler handler)
    {
        return handler.Retrieve(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(StockEntriesRow))]
    public ListResponse<StockEntriesRow> List(IDbConnection connection, ListRequest request,
        [FromServices] IStockEntriesListHandler handler)
    {
        return handler.List(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(StockEntriesRow))]
    public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
        [FromServices] IStockEntriesListHandler handler,
        [FromServices] IExcelExporter exporter)
    {
        var data = List(connection, request, handler).Entities;
        var bytes = exporter.Export(data, typeof(Columns.StockEntriesColumns), request.ExportColumns);
        return ExcelContentResult.Create(bytes, "StokGirisleri_" +
            DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
    }

    [HttpPost, AuthorizeCreate(typeof(StockEntriesRow))]
    public GetNextNumberResponse GetNextEntryNo(IDbConnection connection)
    {
        var template = connection.TryFirst<NumberTemplatesRow>(q => q.SelectTableFields()
            .Where(NumberTemplatesRow.Fields.Type == (int)NumberTemplateType.StokGirisi &
                   NumberTemplatesRow.Fields.Active == 1));

        string prefix;
        int length;

        if (template != null)
        {
            prefix = template.Prefix ?? "SGR";

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
            prefix = "SGR" + DateTime.Now.ToString("yyyyMM");
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
            StockEntriesRow.Fields.EntryNo,
            StockEntriesRow.Fields.Id
        );
    }
}
