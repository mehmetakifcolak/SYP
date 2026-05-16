namespace SYP.Common;

public class DashboardPageModel
{
    public int TotalDealers { get; set; }
    public int ActiveDealers { get; set; }
    public int PassiveDealers { get; set; }
    public int TotalProducts { get; set; }
    public List<StockMovementDto> RecentMovements { get; set; } = new();
    public List<AuditLogDto> RecentAuditLogs { get; set; } = new();
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
