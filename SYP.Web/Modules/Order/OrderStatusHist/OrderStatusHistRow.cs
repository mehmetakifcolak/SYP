namespace SYP.Order;

[ConnectionKey("Default"), Module("Order"), TableName("OrderStatusHistory")]
[DisplayName("Order Status Hist"), InstanceName("Order Status Hist")]
[NavigationPermission("Order:OrderStatusHist:Navigation")]
[ReadPermission("Order:OrderStatusHist:Read")]
[InsertPermission("Order:OrderStatusHist:Insert")]
[UpdatePermission("Order:OrderStatusHist:Update")]
[DeletePermission("Order:OrderStatusHist:Delete")]
[ServiceLookupPermission("Order:OrderStatusHist:Lookup")]
public sealed class OrderStatusHistRow : Row<OrderStatusHistRow.RowFields>, IIdRow, INameRow
{
    const string jOrder = nameof(jOrder);
    const string jChangedByUser = nameof(jChangedByUser);

    [DisplayName("Id"), Identity, IdProperty]
    public long? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int64Field Id; }
    
    [DisplayName("Order"), NotNull, ForeignKey(typeof(OrderRow)), LeftJoin(jOrder), TextualField(nameof(OrderNumber))]
    [ServiceLookupEditor(typeof(OrderRow))]
    public int? OrderId { get => fields.OrderId[this]; set => fields.OrderId[this] = value; }
    public partial class RowFields { public Int32Field OrderId; }
    
    [DisplayName("Old Status")]
    public int? OldStatus { get => fields.OldStatus[this]; set => fields.OldStatus[this] = value; }
    public partial class RowFields { public Int32Field OldStatus; }
    
    [DisplayName("New Status"), NotNull]
    public int? NewStatus { get => fields.NewStatus[this]; set => fields.NewStatus[this] = value; }
    public partial class RowFields { public Int32Field NewStatus; }
    
    [DisplayName("Changed By User"), NotNull, ForeignKey(typeof(Administration.UserRow)), LeftJoin(jChangedByUser)]
    [TextualField(nameof(ChangedByUserUsername)), LookupEditor(typeof(Administration.UserRow), Async = true)]
    public int? ChangedByUserId { get => fields.ChangedByUserId[this]; set => fields.ChangedByUserId[this] = value; }
    public partial class RowFields { public Int32Field ChangedByUserId; }
    
    [DisplayName("Changed By User Role"), Size(50), QuickSearch, NameProperty]
    public string ChangedByUserRole { get => fields.ChangedByUserRole[this]; set => fields.ChangedByUserRole[this] = value; }
    public partial class RowFields { public StringField ChangedByUserRole; }
    
    [DisplayName("Change Reason")]
    public string ChangeReason { get => fields.ChangeReason[this]; set => fields.ChangeReason[this] = value; }
    public partial class RowFields { public StringField ChangeReason; }
    
    [DisplayName("Change Date"), NotNull]
    public DateTime? ChangeDate { get => fields.ChangeDate[this]; set => fields.ChangeDate[this] = value; }
    public partial class RowFields { public DateTimeField ChangeDate; }
    
    #region Foreign Fields

    [DisplayName("Order Order Number"), Origin(jOrder, nameof(OrderRow.OrderNumber)), ReadOnly(true)]
    public string OrderNumber { get => fields.OrderNumber[this]; set => fields.OrderNumber[this] = value; }
    public partial class RowFields { public StringField OrderNumber; }

    [DisplayName("Changed By User Username"), Origin(jChangedByUser, nameof(Administration.UserRow.Username)), ReadOnly(true)]
    public string ChangedByUserUsername { get => fields.ChangedByUserUsername[this]; set => fields.ChangedByUserUsername[this] = value; }
    public partial class RowFields { public StringField ChangedByUserUsername; }

    #endregion Foreign Fields

    public partial class RowFields : RowFieldsBase { }
}