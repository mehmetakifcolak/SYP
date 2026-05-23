namespace SYP.Order;

public class BayiOrdersPageModel
{
    public string CustomerName { get; set; } = "";
    public int TotalOrders { get; set; }
    public int ActiveOrders { get; set; }
    public int PendingApproval { get; set; }
    public int DeliveredOrders { get; set; }
    public int CancelledOrders { get; set; }
    public List<BayiOrderStatusCountDto> StatusCounts { get; set; } = new();
    public List<BayiRecentOrderDto> RecentOrders { get; set; } = new();
}

public class BayiOrderStatusCountDto
{
    public int Status { get; set; }
    public int Count { get; set; }
}

public class BayiRecentOrderDto
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = "";
    public DateTime OrderDate { get; set; }
    public decimal NetAmount { get; set; }
    public string CurrencyCode { get; set; } = "TRY";
    public int Status { get; set; }
}
