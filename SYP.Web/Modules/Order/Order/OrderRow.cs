namespace SYP.Order;

[ConnectionKey("Default"), Module("Order"), TableName("Orders")]
[DisplayName("Order"), InstanceName("Order")]
[NavigationPermission("Order:Order:Navigation")]
[ReadPermission("Order:Order:Read")]
[InsertPermission("Order:Order:Insert")]
[UpdatePermission("Order:Order:Update")]
[DeletePermission("Order:Order:Delete")]
[ServiceLookupPermission("Order:Order:Lookup")]
public sealed class OrderRow : Row<OrderRow.RowFields>, IIdRow, INameRow
{
    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }

    [DisplayName("Sipariş No"), Size(50), NotNull, QuickSearch, NameProperty]
    public string OrderNumber { get => fields.OrderNumber[this]; set => fields.OrderNumber[this] = value; }
    public partial class RowFields { public StringField OrderNumber; }

    [DisplayName("Bayi"), NotNull, ForeignKey("[dbo].[Customers]", "Id"), LeftJoin("jCustomer")]
    [ServiceLookupEditor(typeof(Customer.CustomersRow), Service = "Customer/Customers/List")]
    public int? CustomerId { get => fields.CustomerId[this]; set => fields.CustomerId[this] = value; }
    public partial class RowFields { public Int32Field CustomerId; }

    [DisplayName("Sorumlu Yönetici"), ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jManager")]
    [LookupEditor("Administration.User")]
    public int? ManagerUserId { get => fields.ManagerUserId[this]; set => fields.ManagerUserId[this] = value; }
    public partial class RowFields { public Int32Field ManagerUserId; }

    [DisplayName("Durum"), NotNull, DefaultValue(1)]
    public OrderStatus? Status { get => (OrderStatus?)fields.Status[this]; set => fields.Status[this] = (int?)value; }
    public partial class RowFields { public Int32Field Status; }

    [DisplayName("Sipariş Tarihi"), NotNull]
    public DateTime? OrderDate { get => fields.OrderDate[this]; set => fields.OrderDate[this] = value; }
    public partial class RowFields { public DateTimeField OrderDate; }

    [DisplayName("Toplam Tutar"), NotNull, DefaultValue(0), Scale(4)]
    public decimal? TotalAmount { get => fields.TotalAmount[this]; set => fields.TotalAmount[this] = value; }
    public partial class RowFields { public DecimalField TotalAmount; }

    [DisplayName("İndirim Oranı (%)"), Scale(2)]
    public decimal? DiscountPercentage { get => fields.DiscountPercentage[this]; set => fields.DiscountPercentage[this] = value; }
    public partial class RowFields { public DecimalField DiscountPercentage; }

    [DisplayName("İndirim Tutarı"), NotNull, DefaultValue(0), Scale(4)]
    public decimal? DiscountAmount { get => fields.DiscountAmount[this]; set => fields.DiscountAmount[this] = value; }
    public partial class RowFields { public DecimalField DiscountAmount; }

    [DisplayName("Net Tutar"), NotNull, DefaultValue(0), Scale(4)]
    public decimal? NetAmount { get => fields.NetAmount[this]; set => fields.NetAmount[this] = value; }
    public partial class RowFields { public DecimalField NetAmount; }

    [DisplayName("Para Birimi"), ForeignKey("[dbo].[CurrencyList]", "Id"), LeftJoin("jCurrency")]
    [LookupEditor("Setting.CurrencyList")]
    public int? CurrencyId { get => fields.CurrencyId[this]; set => fields.CurrencyId[this] = value; }
    public partial class RowFields { public Int32Field CurrencyId; }

    [DisplayName("Notlar"), Size(int.MaxValue)]
    public string Notes { get => fields.Notes[this]; set => fields.Notes[this] = value; }
    public partial class RowFields { public StringField Notes; }

    [DisplayName("Red Nedeni"), Size(int.MaxValue)]
    public string RejectReason { get => fields.RejectReason[this]; set => fields.RejectReason[this] = value; }
    public partial class RowFields { public StringField RejectReason; }

    [DisplayName("Kayıt Tarihi"), Insertable(false), Updatable(false)]
    public DateTime? InsertDate { get => fields.InsertDate[this]; set => fields.InsertDate[this] = value; }
    public partial class RowFields { public DateTimeField InsertDate; }

    [DisplayName("Kaydeden"), Insertable(false), Updatable(false)]
    public int? InsertUserId { get => fields.InsertUserId[this]; set => fields.InsertUserId[this] = value; }
    public partial class RowFields { public Int32Field InsertUserId; }

    [DisplayName("Güncelleme Tarihi"), Insertable(false), Updatable(false)]
    public DateTime? UpdateDate { get => fields.UpdateDate[this]; set => fields.UpdateDate[this] = value; }
    public partial class RowFields { public DateTimeField UpdateDate; }

    [DisplayName("Güncelleyen"), Insertable(false), Updatable(false)]
    public int? UpdateUserId { get => fields.UpdateUserId[this]; set => fields.UpdateUserId[this] = value; }
    public partial class RowFields { public Int32Field UpdateUserId; }

    #region Foreign Fields

    [DisplayName("Bayi Adı"), Expression("jCustomer.[Name]")]
    public string CustomerName { get => fields.CustomerName[this]; set => fields.CustomerName[this] = value; }
    public partial class RowFields { public StringField CustomerName; }

    [DisplayName("Bayi Kodu"), Expression("jCustomer.[Code]")]
    public string CustomerCode { get => fields.CustomerCode[this]; set => fields.CustomerCode[this] = value; }
    public partial class RowFields { public StringField CustomerCode; }

    [DisplayName("Yönetici Adı"), Expression("jManager.[DisplayName]")]
    public string ManagerName { get => fields.ManagerName[this]; set => fields.ManagerName[this] = value; }
    public partial class RowFields { public StringField ManagerName; }

    [DisplayName("Para Birimi Kodu"), Expression("jCurrency.[Code]")]
    public string CurrencyCode { get => fields.CurrencyCode[this]; set => fields.CurrencyCode[this] = value; }
    public partial class RowFields { public StringField CurrencyCode; }

    #endregion Foreign Fields

    #region Master-Detail

    [DisplayName("Sipariş Kalemleri"), NotMapped]
    [MasterDetailRelation(foreignKey: "OrderId", IncludeColumns = "ProductId,Quantity,UnitId,UnitPrice,VatRateId,VatRate,Discount,LineTotal,Notes")]
    public List<OrderDetailRow> DetailList { get => fields.DetailList[this]; set => fields.DetailList[this] = value; }
    public partial class RowFields { public RowListField<OrderDetailRow> DetailList; }

    #endregion Master-Detail

    public partial class RowFields : RowFieldsBase { }
}
