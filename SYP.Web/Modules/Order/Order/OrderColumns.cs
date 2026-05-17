using Serenity.ComponentModel;
using System;

namespace SYP.Order.Columns;

[ColumnsScript("Order.Order")]
[BasedOnRow(typeof(OrderRow), CheckNames = true)]
public class OrderColumns
{
    [EditLink, Width(150)]
    public string OrderNumber { get; set; }

    [Width(200)]
    public string CustomerName { get; set; }

    [Width(150)]
    public string CustomerCode { get; set; }

    [Width(150)]
    public string ManagerName { get; set; }

    [Width(120)]
    public DateTime OrderDate { get; set; }

    [Width(150)]
    public OrderStatus Status { get; set; }

    [Width(120), AlignRight, DisplayFormat("#,##0.00")]
    public decimal TotalAmount { get; set; }

    [Width(100), AlignRight, DisplayFormat("#,##0.00")]
    public decimal DiscountPercentage { get; set; }

    [Width(120), AlignRight, DisplayFormat("#,##0.00")]
    public decimal DiscountAmount { get; set; }

    [Width(120), AlignRight, DisplayFormat("#,##0.00")]
    public decimal NetAmount { get; set; }

    [Width(80)]
    public string CurrencyCode { get; set; }
}
