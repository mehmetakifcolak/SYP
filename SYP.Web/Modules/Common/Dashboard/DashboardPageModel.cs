namespace SYP.Common;

public class DashboardPageModel
{
    // Müşteri İstatistikleri
    public int TotalDealers { get; set; }
    public int ActiveDealers { get; set; }
    public int PassiveDealers { get; set; }
    public int NewDealersLast30Days { get; set; }

    // Ürün İstatistikleri
    public int TotalProducts { get; set; }
    public int TotalBrands { get; set; }
    public int TotalCategories { get; set; }

    // Depo İstatistikleri
    public int TotalWarehouses { get; set; }
    public decimal TotalStockValue { get; set; }
    public int TodayEntries { get; set; }
    public int TodayExits { get; set; }
    public int WeekEntries { get; set; }
    public int WeekExits { get; set; }

    // Fiyat Listesi
    public int ActivePriceLists { get; set; }
    public int ExpiringPriceLists { get; set; }

    // Listeler
    public List<StockMovementDto> RecentMovements { get; set; } = new();
    public List<AuditLogDto> RecentAuditLogs { get; set; } = new();
    public List<LowStockDto> LowStockAlerts { get; set; } = new();
    public List<WarehouseStockDto> StockByWarehouse { get; set; } = new();
    public List<TopProductDto> TopMovingProducts { get; set; } = new();
    public List<CategoryProductCountDto> ProductsByCategory { get; set; } = new();
    public List<BrandProductCountDto> ProductsByBrand { get; set; } = new();
    public List<ExchangeRateDto> ExchangeRates { get; set; } = new();
    public List<ExpiringPriceListDto> ExpiringPriceListDetails { get; set; } = new();
}

public class StockMovementDto
{
    public string Id { get; set; }
    public string MovementType { get; set; }
    public string DocumentNo { get; set; }
    public string WarehouseName { get; set; }
    public string ProductCode { get; set; }
    public string ProductName { get; set; }
    public decimal Quantity { get; set; }
    public DateTime MovementDate { get; set; }
    public short Status { get; set; }
}

public class AuditLogDto
{
    public long Id { get; set; }
    public string EntityType { get; set; }
    public string EntityName { get; set; }
    public string ActionType { get; set; }
    public DateTime ActionDate { get; set; }
    public string Username { get; set; }
    public string IpAddress { get; set; }
}

public class LowStockDto
{
    public string ProductCode { get; set; }
    public string ProductName { get; set; }
    public string WarehouseName { get; set; }
    public decimal Quantity { get; set; }
}

public class WarehouseStockDto
{
    public string WarehouseName { get; set; }
    public decimal TotalQuantity { get; set; }
    public int ProductCount { get; set; }
}

public class TopProductDto
{
    public string ProductCode { get; set; }
    public string ProductName { get; set; }
    public int MovementCount { get; set; }
    public decimal TotalQuantity { get; set; }
}

public class CategoryProductCountDto
{
    public string CategoryName { get; set; }
    public int ProductCount { get; set; }
}

public class BrandProductCountDto
{
    public string BrandName { get; set; }
    public int ProductCount { get; set; }
}

public class ExchangeRateDto
{
    public string CurrencyCode { get; set; }
    public decimal? ForexBuying { get; set; }
    public decimal? ForexSelling { get; set; }
    public DateTime? InsertDate { get; set; }
}

public class ExpiringPriceListDto
{
    public string Code { get; set; }
    public string Name { get; set; }
    public DateTime? ValidTo { get; set; }
    public int DaysLeft { get; set; }
}
