using Microsoft.AspNetCore.Mvc;
using Serenity.Data;
using Serenity.Services;
using System.Data;
using MyRow = SYP.Catalog.PriceListItemsRow;

namespace SYP.Catalog.Endpoints;

[Route("Services/Catalog/PriceListItems/[action]")]
[ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
public class PriceListItemsEndpoint : ServiceEndpoint
{
    [HttpPost]
    public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
        [FromServices] IPriceListItemsRetrieveHandler handler)
    {
        return handler.Retrieve(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(MyRow))]
    public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
        [FromServices] IPriceListItemsListHandler handler)
    {
        return handler.List(connection, request);
    }
}
