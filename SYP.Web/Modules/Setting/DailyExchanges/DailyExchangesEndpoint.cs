using Serenity.Reporting;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Xml.Linq;
using MyRow = SYP.Setting.DailyExchangesRow;

namespace SYP.Setting.Endpoints;

[Route("Services/Setting/DailyExchanges/[action]")]
[ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
public class DailyExchangesEndpoint : ServiceEndpoint
{
    [HttpPost, AuthorizeCreate(typeof(MyRow))]
    public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
        [FromServices] IDailyExchangesSaveHandler handler)
    {
        return handler.Create(uow, request);
    }

    [HttpPost, AuthorizeUpdate(typeof(MyRow))]
    public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
        [FromServices] IDailyExchangesSaveHandler handler)
    {
        return handler.Update(uow, request);
    }
 
    [HttpPost, AuthorizeDelete(typeof(MyRow))]
    public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
        [FromServices] IDailyExchangesDeleteHandler handler)
    {
        return handler.Delete(uow, request);
    }

    [HttpPost, AuthorizeRetrieve(typeof(MyRow))]
    public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
        [FromServices] IDailyExchangesRetrieveHandler handler)
    {
        return handler.Retrieve(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(MyRow))]
    public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
        [FromServices] IDailyExchangesListHandler handler)
    {
        return handler.List(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(MyRow))]
    public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
        [FromServices] IDailyExchangesListHandler handler,
        [FromServices] IExcelExporter exporter)
    {
        var data = List(connection, request, handler).Entities;
        var bytes = exporter.Export(data, typeof(Columns.DailyExchangesColumns), request.ExportColumns);
        return ExcelContentResult.Create(bytes, "DailyExchangesList_" +
            DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
    }

    [HttpPost, AuthorizeCreate(typeof(MyRow))]
    public ServiceResponse ImportDateRange(IUnitOfWork uow, ImportDateRangeRequest request)
    {
        if (request.StartDate == null || request.EndDate == null)
            throw new ValidationError("Baslangic ve Bitis tarihi zorunludur.");

        if (request.StartDate > request.EndDate)
            throw new ValidationError("Baslangic tarihi bitis tarihinden sonra olamaz.");

        if ((request.EndDate.Value - request.StartDate.Value).TotalDays > 365)
            throw new ValidationError("Tarih araligi 365 gunu asamaz.");

        var connection = uow.Connection;
        var cachedCurrencies = connection.List<CurrencyListRow>()
            .Where(x => x.IsActive == true).ToList();
        var fld = MyRow.Fields;

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");

        for (var date = request.StartDate.Value.Date; date <= request.EndDate.Value.Date; date = date.AddDays(1))
        {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                continue;

            try
            {
                var yearMonth = date.ToString("yyyyMM");
                var dayMonthYear = date.ToString("ddMMyyyy");
                var url = $"https://www.tcmb.gov.tr/kurlar/{yearMonth}/{dayMonthYear}.xml";

                var content = client.GetStringAsync(url).GetAwaiter().GetResult();
                if (string.IsNullOrWhiteSpace(content))
                    continue;

                var doc = XDocument.Parse(content);
                var currencies = doc.Descendants("Currency");

                foreach (var currencyElement in currencies)
                {
                    var code = currencyElement.Attribute("CurrencyCode")?.Value;
                    if (string.IsNullOrEmpty(code))
                        continue;

                    var currencyDef = cachedCurrencies.FirstOrDefault(x =>
                        string.Equals(x.Code, code, StringComparison.OrdinalIgnoreCase));
                    if (currencyDef == null || currencyDef.Id == null)
                        continue;

                    var nextDay = date.AddDays(1);
                    var criteria = new Criteria(fld.CurrencyId) == currencyDef.Id.Value &
                                   new Criteria(fld.InsertDate) >= date &
                                   new Criteria(fld.InsertDate) < nextDay;

                    if (connection.TryFirst<MyRow>(criteria) != null)
                        continue;

                    connection.Insert(new MyRow
                    {
                        CurrencyId = currencyDef.Id.Value,
                        CurrencyCode = code,
                        ForexBuying = ParseDecimal(currencyElement.Element("ForexBuying")?.Value),
                        ForexSelling = ParseDecimal(currencyElement.Element("ForexSelling")?.Value),
                        BanknoteBuying = ParseDecimal(currencyElement.Element("BanknoteBuying")?.Value),
                        BanknoteSelling = ParseDecimal(currencyElement.Element("BanknoteSelling")?.Value),
                        InsertDate = date,
                        IsActive = true
                    });
                }
            }
            catch
            {
                continue;
            }
        }

        return new ServiceResponse();
    }

    [HttpPost, AuthorizeCreate(typeof(MyRow))]
    public ServiceResponse FetchTodayRates(IUnitOfWork uow)
    {
        var connection = uow.Connection;
        var cachedCurrencies = connection.List<CurrencyListRow>()
            .Where(x => x.IsActive == true).ToList();
        var fld = MyRow.Fields;

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");

        var url = "https://www.tcmb.gov.tr/kurlar/today.xml";
        var content = client.GetStringAsync(url).GetAwaiter().GetResult();

        if (string.IsNullOrWhiteSpace(content))
            throw new ValidationError("TCMB API'den veri alinamadi.");

        var doc = XDocument.Parse(content);
        var currencies = doc.Descendants("Currency");
        var now = DateTime.Now;
        var today = now.Date;
        var tomorrow = today.AddDays(1);

        foreach (var currencyElement in currencies)
        {
            var code = currencyElement.Attribute("CurrencyCode")?.Value;
            if (string.IsNullOrEmpty(code))
                continue;

            var currencyDef = cachedCurrencies.FirstOrDefault(x =>
                string.Equals(x.Code, code, StringComparison.OrdinalIgnoreCase));
            if (currencyDef == null || currencyDef.Id == null)
                continue;

            var criteria = new Criteria(fld.CurrencyId) == currencyDef.Id.Value &
                           new Criteria(fld.InsertDate) >= today &
                           new Criteria(fld.InsertDate) < tomorrow;

            if (connection.TryFirst<MyRow>(criteria) != null)
                continue;

            connection.Insert(new MyRow
            {
                CurrencyId = currencyDef.Id.Value,
                CurrencyCode = code,
                ForexBuying = ParseDecimal(currencyElement.Element("ForexBuying")?.Value),
                ForexSelling = ParseDecimal(currencyElement.Element("ForexSelling")?.Value),
                BanknoteBuying = ParseDecimal(currencyElement.Element("BanknoteBuying")?.Value),
                BanknoteSelling = ParseDecimal(currencyElement.Element("BanknoteSelling")?.Value),
                InsertDate = now,
                IsActive = true
            });
        }

        return new ServiceResponse();
    }

    private static decimal? ParseDecimal(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;
        if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
            return result;
        return null;
    }
}

[DataContract]
public class ImportDateRangeRequest : ServiceRequest
{
    [DataMember]
    public DateTime? StartDate { get; set; }
    [DataMember]
    public DateTime? EndDate { get; set; }
}