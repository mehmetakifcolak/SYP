using Serenity.Reporting;
using System.Data;
using System.Globalization;
using System.Runtime.Serialization;
using MyRow = SYP.Catalog.ProductsRow;

namespace SYP.Catalog.Endpoints;

[Route("Services/Catalog/Products/[action]")]
[ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
public class ProductsEndpoint : ServiceEndpoint
{
    [HttpPost, AuthorizeCreate(typeof(MyRow))]
    public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
        [FromServices] IProductsSaveHandler handler)
    {
        return handler.Create(uow, request);
    }

    [HttpPost, AuthorizeUpdate(typeof(MyRow))]
    public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
        [FromServices] IProductsSaveHandler handler)
    {
        return handler.Update(uow, request);
    }
 
    [HttpPost, AuthorizeDelete(typeof(MyRow))]
    public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
        [FromServices] IProductsDeleteHandler handler)
    {
        return handler.Delete(uow, request);
    }

    [HttpPost, AuthorizeRetrieve(typeof(MyRow))]
    public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
        [FromServices] IProductsRetrieveHandler handler)
    {
        return handler.Retrieve(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(MyRow))]
    public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
        [FromServices] IProductsListHandler handler)
    {
        return handler.List(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(MyRow))]
    public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
        [FromServices] IProductsListHandler handler,
        [FromServices] IExcelExporter exporter)
    {
        var data = List(connection, request, handler).Entities;
        var bytes = exporter.Export(data, typeof(Columns.ProductsColumns), request.ExportColumns);
        return ExcelContentResult.Create(bytes, "ProductsList_" +
            DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
    }

    [HttpPost, AuthorizeCreate(typeof(MyRow))]
    public BulkImportProductsResponse BulkImportProducts(
        IUnitOfWork uow,
        BulkImportProductsRequest request,
        [FromServices] IProductsSaveHandler handler)
    {
        var response = new BulkImportProductsResponse
        {
            SuccessCount = 0,
            ErrorCount = 0,
            Errors = new List<ProductImportError>()
        };

        if (request.Products == null || request.Products.Count == 0)
        {
            throw new ValidationError("İçe aktarılacak ürün bulunamadı.");
        }

        foreach (var product in request.Products)
        {
            try
            {
                var saveRequest = new SaveRequest<MyRow>
                {
                    Entity = new MyRow
                    {
                        Code = product.Code,
                        Name = product.Name,
                        Name2 = product.Name2,
                        Description = product.Description,
                        Barcode = product.Barcode,
                        UnitPrice = product.UnitPrice,
                        CategoryId = product.CategoryId,
                        BrandId = product.BrandId,
                        UnitId = product.UnitId,
                        CurrencyId = product.CurrencyId,
                        VatRateId = product.VatRateId,
                        PackingId = product.PackingId,
                        IsActive = 1
                    }
                };

                handler.Create(uow, saveRequest);
                response.SuccessCount++;
            }
            catch (ValidationError ve)
            {
                response.ErrorCount++;
                response.Errors.Add(new ProductImportError
                {
                    RowIndex = product.RowIndex,
                    ProductCode = product.Code,
                    ErrorMessage = ve.Message
                });
            }
            catch (Exception ex)
            {
                response.ErrorCount++;
                response.Errors.Add(new ProductImportError
                {
                    RowIndex = product.RowIndex,
                    ProductCode = product.Code,
                    ErrorMessage = "Beklenmeyen hata: " + ex.Message
                });
            }
        }

        return response;
    }
}

[DataContract]
public class BulkImportProductsRequest : ServiceRequest
{
    [DataMember]
    public List<ProductImportItem> Products { get; set; }
}

[DataContract]
public class ProductImportItem
{
    [DataMember]
    public int RowIndex { get; set; }

    [DataMember]
    public string Code { get; set; }

    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public string Name2 { get; set; }

    [DataMember]
    public string Description { get; set; }

    [DataMember]
    public string Barcode { get; set; }

    [DataMember]
    public decimal? UnitPrice { get; set; }

    [DataMember]
    public int? CategoryId { get; set; }

    [DataMember]
    public int? BrandId { get; set; }

    [DataMember]
    public int? UnitId { get; set; }

    [DataMember]
    public int? CurrencyId { get; set; }

    [DataMember]
    public int? VatRateId { get; set; }

    [DataMember]
    public int? PackingId { get; set; }
}

[DataContract]
public class BulkImportProductsResponse : ServiceResponse
{
    [DataMember]
    public int SuccessCount { get; set; }

    [DataMember]
    public int ErrorCount { get; set; }

    [DataMember]
    public List<ProductImportError> Errors { get; set; }
}

[DataContract]
public class ProductImportError
{
    [DataMember]
    public int RowIndex { get; set; }

    [DataMember]
    public string ProductCode { get; set; }

    [DataMember]
    public string ErrorMessage { get; set; }
}