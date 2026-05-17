namespace SYP.Order;

[ConnectionKey("Default"), Module("Order"), TableName("TieredDiscountSettings")]
[DisplayName("Tiered Discount Settings"), InstanceName("Tiered Discount Settings")]
[NavigationPermission("Order:TieredDiscountSettings:Navigation")]
[ReadPermission("Order:TieredDiscountSettings:Read")]
[InsertPermission("Order:TieredDiscountSettings:Insert")]
[UpdatePermission("Order:TieredDiscountSettings:Update")]
[DeletePermission("Order:TieredDiscountSettings:Delete")]
public sealed class TieredDiscountSettingsRow : Row<TieredDiscountSettingsRow.RowFields>, IIdRow
{
    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }
    
    [DisplayName("Min Amount"), Size(18), Scale(4), NotNull]
    public decimal? MinAmount { get => fields.MinAmount[this]; set => fields.MinAmount[this] = value; }
    public partial class RowFields { public DecimalField MinAmount; }
    
    [DisplayName("Discount Percentage"), Size(5), Scale(2), NotNull]
    public decimal? DiscountPercentage { get => fields.DiscountPercentage[this]; set => fields.DiscountPercentage[this] = value; }
    public partial class RowFields { public DecimalField DiscountPercentage; }
    
    [DisplayName("Is Active"), NotNull]
    public bool? IsActive { get => fields.IsActive[this]; set => fields.IsActive[this] = value; }
    public partial class RowFields { public BooleanField IsActive; }
    
    [DisplayName("Display Order"), NotNull]
    public int? DisplayOrder { get => fields.DisplayOrder[this]; set => fields.DisplayOrder[this] = value; }
    public partial class RowFields { public Int32Field DisplayOrder; }
    
    [DisplayName("Insert Date"), NotNull]
    public DateTime? InsertDate { get => fields.InsertDate[this]; set => fields.InsertDate[this] = value; }
    public partial class RowFields { public DateTimeField InsertDate; }
    
    [DisplayName("Insert User Id"), NotNull]
    public int? InsertUserId { get => fields.InsertUserId[this]; set => fields.InsertUserId[this] = value; }
    public partial class RowFields { public Int32Field InsertUserId; }
    
    [DisplayName("Update Date")]
    public DateTime? UpdateDate { get => fields.UpdateDate[this]; set => fields.UpdateDate[this] = value; }
    public partial class RowFields { public DateTimeField UpdateDate; }
    
    [DisplayName("Update User Id")]
    public int? UpdateUserId { get => fields.UpdateUserId[this]; set => fields.UpdateUserId[this] = value; }
    public partial class RowFields { public Int32Field UpdateUserId; }
    
    #region Foreign Fields

    #endregion Foreign Fields

    public partial class RowFields : RowFieldsBase { }
}