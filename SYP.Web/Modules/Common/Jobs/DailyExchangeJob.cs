using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serenity.Data;
using SYP.Setting;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SYP.Common.Jobs;

public class DailyExchangeJob : BackgroundService
{
    private readonly IServiceProvider serviceProvider;
    private readonly ILogger<DailyExchangeJob> logger;

    public DailyExchangeJob(IServiceProvider serviceProvider, ILogger<DailyExchangeJob> logger)
    {
        this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await FetchDailyExchangeRates();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in DailyExchangeJob");
            }

            // Her gun saat 09:00'da calistir, yoksa her 1 saatte bir kontrol et
            var now = DateTime.Now;
            var nextRun = now.Date.AddHours(9);
            if (now > nextRun)
                nextRun = nextRun.AddDays(1);

            var delay = nextRun - now;
            if (delay.TotalMinutes < 1)
                delay = TimeSpan.FromHours(1);

            await Task.Delay(delay, stoppingToken);
        }
    }

    public async Task FetchDailyExchangeRates()
    {
        try
        {
            var url = "https://www.tcmb.gov.tr/kurlar/today.xml";
            string content;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
                content = await client.GetStringAsync(url);
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                logger.LogWarning("TCMB API returned empty response");
                return;
            }

            var doc = XDocument.Parse(content);
            var currencies = doc.Descendants("Currency");
            var now = DateTime.Now;
            var today = now.Date;
            var tomorrow = today.AddDays(1);

            using var scope = serviceProvider.CreateScope();
            var sqlConnections = scope.ServiceProvider.GetRequiredService<ISqlConnections>();

            using var connection = sqlConnections.NewByKey("Default");
            var cachedCurrencies = connection.List<CurrencyListRow>()
                .Where(x => x.IsActive == true).ToList();
            var fld = DailyExchangesRow.Fields;

            foreach (var currencyElement in currencies)
            {
                var code = currencyElement.Attribute("CurrencyCode")?.Value;
                if (string.IsNullOrEmpty(code))
                    continue;

                var currencyDef = cachedCurrencies.FirstOrDefault(x =>
                    string.Equals(x.Code, code, StringComparison.OrdinalIgnoreCase));
                if (currencyDef == null || currencyDef.Id == null)
                    continue;

                var forexBuying = ParseDecimal(currencyElement.Element("ForexBuying")?.Value);
                var forexSelling = ParseDecimal(currencyElement.Element("ForexSelling")?.Value);
                var banknoteBuying = ParseDecimal(currencyElement.Element("BanknoteBuying")?.Value);
                var banknoteSelling = ParseDecimal(currencyElement.Element("BanknoteSelling")?.Value);

                var criteria = new Criteria(fld.CurrencyId) == currencyDef.Id.Value &
                               new Criteria(fld.InsertDate) >= today &
                               new Criteria(fld.InsertDate) < tomorrow;

                if (connection.TryFirst<DailyExchangesRow>(criteria) == null)
                {
                    connection.Insert(new DailyExchangesRow
                    {
                        CurrencyId = currencyDef.Id.Value,
                        CurrencyCode = code,
                        ForexBuying = forexBuying,
                        ForexSelling = forexSelling,
                        BanknoteBuying = banknoteBuying,
                        BanknoteSelling = banknoteSelling,
                        InsertDate = now,
                        IsActive = true
                    });

                    logger.LogInformation("Exchange rate for {Code} saved: Forex Buying={ForexBuying}", code, forexBuying);
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching exchange rates from TCMB");
            throw;
        }
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
