namespace SYP.Order;

[ConnectionKey("Default"), Module("Order"), TableName("Orders")]
[DisplayName("Order"), InstanceName("Order")]
[NavigationPermission("Order:Order:Navigation")]
[ReadPermission("Order:Order:Read")]
[InsertPermission("Order:Order:Insert")]
[UpdatePermission("Order:Order:Update")]
[DeletePermission("Order:Order:Delete")]
[ServiceLookupPermission("Order:Order:Lookup")]
public sealed class OrderRow : Row<OrderRow.RowFields>, IIdRow, INameRow, SYP.Administration.IAuditedRow,
    IInsertLogRow, IUpdateLogRow, IIsDeletedRow, IDeleteLogRow
{
    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }

    [DisplayName("Sipariş No"), Size(50), NotNull, QuickSearch, NameProperty, Insertable(false)]
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

    [DisplayName("Durum"), NotNull, DefaultValue(14),QuickFilter]
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

    [DisplayName("Depo"), ForeignKey("[dbo].[Warehouses]", "Id"), LeftJoin("jWarehouse")]
    [LookupEditor("Warehouse.Warehouses", FilterField = "IsActive", FilterValue = true)]
    public int? WarehouseId { get => fields.WarehouseId[this]; set => fields.WarehouseId[this] = value; }
    public partial class RowFields { public Int32Field WarehouseId; }

    [DisplayName("Notlar"), Size(int.MaxValue)]
    public string Notes { get => fields.Notes[this]; set => fields.Notes[this] = value; }
    public partial class RowFields { public StringField Notes; }

    [DisplayName("Stok Çıkışı Oluşturuldu"), DefaultValue(false), Insertable(false), Updatable(false)]
    public bool? IsStockExitCreated { get => fields.IsStockExitCreated[this]; set => fields.IsStockExitCreated[this] = value; }
    public partial class RowFields { public BooleanField IsStockExitCreated; }

    [DisplayName("Silindi"), DefaultValue(false), Insertable(false), Updatable(false)]
    public bool? IsDeleted { get => fields.IsDeleted[this]; set => fields.IsDeleted[this] = value; }
    public partial class RowFields { public BooleanField IsDeleted; }

    [DisplayName("Silinme Tarihi"), Insertable(false), Updatable(false)]
    public DateTime? DeletedDate { get => fields.DeletedDate[this]; set => fields.DeletedDate[this] = value; }
    public partial class RowFields { public DateTimeField DeletedDate; }

    [DisplayName("Silen Kullanıcı"), Insertable(false), Updatable(false)]
    public int? DeletedUserId { get => fields.DeletedUserId[this]; set => fields.DeletedUserId[this] = value; }
    public partial class RowFields { public Int32Field DeletedUserId; }

    [DisplayName("Red Nedeni"), Size(int.MaxValue)]
    public string RejectReason { get => fields.RejectReason[this]; set => fields.RejectReason[this] = value; }
    public partial class RowFields { public StringField RejectReason; }

    [DisplayName("Kayıt Tarihi"), Insertable(false), Updatable(false)]
    public DateTime? InsertDate { get => fields.InsertDate[this]; set => fields.InsertDate[this] = value; }
    public partial class RowFields { public DateTimeField InsertDate; }

    [DisplayName("Kaydeden"), Insertable(false), Updatable(false)]
    public int? InsertUserId { get => fields.InsertUserId[this]; set => fields.InsertUserId[this] = value; }
    public partial class RowFields { public Int32Field InsertUserId; }

    [DisplayName("Güncelleme Tarihi"), Insertable(false)]
    public DateTime? UpdateDate { get => fields.UpdateDate[this]; set => fields.UpdateDate[this] = value; }
    public partial class RowFields { public DateTimeField UpdateDate; }

    [DisplayName("Güncelleyen"), Insertable(false)]
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

    [DisplayName("Depo Adı"), Expression("jWarehouse.[Name]")]
    public string WarehouseName { get => fields.WarehouseName[this]; set => fields.WarehouseName[this] = value; }
    public partial class RowFields { public StringField WarehouseName; }

    #endregion Foreign Fields

    #region Master-Detail

    [DisplayName("Sipariş Kalemleri"), NotMapped]
    [MasterDetailRelation(foreignKey: "OrderId", IncludeColumns = "ProductId,Quantity,UnitId,UnitPrice,VatRateId,VatRate,Discount,LineTotal,Notes")]
    public List<OrderDetailRow> DetailList { get => fields.DetailList[this]; set => fields.DetailList[this] = value; }
    public partial class RowFields { public RowListField<OrderDetailRow> DetailList; }

    #endregion Master-Detail

    DateTimeField IInsertDateRow.InsertDateField => fields.InsertDate;
    Field IInsertUserIdRow.InsertUserIdField => fields.InsertUserId;
    DateTimeField IUpdateDateRow.UpdateDateField => fields.UpdateDate;
    Field IUpdateUserIdRow.UpdateUserIdField => fields.UpdateUserId;
    BooleanField IIsDeletedRow.IsDeletedField => fields.IsDeleted;
    Field IDeleteLogRow.DeleteUserIdField => fields.DeletedUserId;
    DateTimeField IDeleteLogRow.DeleteDateField => fields.DeletedDate;

    public partial class RowFields : RowFieldsBase { }
}
