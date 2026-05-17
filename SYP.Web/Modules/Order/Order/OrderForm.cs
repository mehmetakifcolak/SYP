using Serenity.ComponentModel;
using System;

namespace SYP.Order.Forms;

[FormScript("Order.Order")]
[BasedOnRow(typeof(OrderRow), CheckNames = true)]
public class OrderForm
{
    [Tab("Genel Bilgiler")]
    [Category("Sipariş Bilgileri")]
    [HalfWidth, ReadOnly(true), Placeholder("Otomatik oluşturulacak")]
    public string OrderNumber { get; set; }

    [HalfWidth]
    public int CustomerId { get; set; }

    [HalfWidth, ReadOnly(true)]
    public int ManagerUserId { get; set; }

    [HalfWidth]
    public DateTime OrderDate { get; set; }

    [HalfWidth]
    public OrderStatus Status { get; set; }

    [HalfWidth]
    public int CurrencyId { get; set; }

    [Category("Tutar Bilgileri")]
    [HalfWidth, ReadOnly(true), DisplayFormat("#,##0.00")]
    public decimal TotalAmount { get; set; }

    [HalfWidth, ReadOnly(true), DisplayFormat("#,##0.00")]
    public decimal DiscountPercentage { get; set; }

    [HalfWidth, ReadOnly(true), DisplayFormat("#,##0.00")]
    public decimal DiscountAmount { get; set; }

    [HalfWidth, ReadOnly(true), DisplayFormat("#,##0.00")]
    public decimal NetAmount { get; set; }

    [Tab("Sipariş Kalemleri")]
    [MasterDetailRelation(foreignKey: "OrderId"), DisplayName("Sipariş Kalemleri")]
    public List<OrderDetailRow> DetailList { get; set; }

    [Tab("Notlar ve Açıklamalar")]
    [Category("Notlar")]
    [FullWidth, TextAreaEditor(Rows = 5)]
    public string Notes { get; set; }

    [Category("Red/İptal Nedeni")]
    [FullWidth, TextAreaEditor(Rows = 3)]
    [Hint("Sipariş reddedildiğinde veya iptal edildiğinde zorunludur")]
    public string RejectReason { get; set; }
}
