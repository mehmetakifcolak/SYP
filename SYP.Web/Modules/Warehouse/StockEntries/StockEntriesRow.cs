
namespace SYP.Warehouse;

[ConnectionKey("Default"), Module("Warehouse"), TableName("StockEntries")]
[DisplayName("Stock Entries"), InstanceName("Stock Entry")]
[NavigationPermission("Warehouse:StockEntries:Navigation")]
[ReadPermission("Warehouse:StockEntries:Read")]
[InsertPermission("Warehouse:StockEntries:Insert")]
[UpdatePermission("Warehouse:StockEntries:Update")]
[DeletePermission("Warehouse:StockEntries:Delete")]
public sealed class StockEntriesRow : Row<StockEntriesRow.RowFields>, IIdRow, INameRow, SYP.Administration.IAuditedRow
{
    const string jWarehouse = nameof(jWarehouse);

    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }

    [DisplayName("Entry No"), Size(50), NotNull, QuickSearch, NameProperty]
    public string EntryNo { get => fields.EntryNo[this]; set => fields.EntryNo[this] = value; }
    public partial class RowFields { public StringField EntryNo; }

    [DisplayName("Warehouse"), NotNull, ForeignKey(typeof(WarehousesRow)), LeftJoin(jWarehouse)]
    [LookupEditor(typeof(WarehousesRow))]
    public int? WarehouseId { get => fields.WarehouseId[this]; set => fields.WarehouseId[this] = value; }
    public partial class RowFields { public Int32Field WarehouseId; }

    [DisplayName("Entry Date"), NotNull]
    public DateTime? EntryDate { get => fields.EntryDate[this]; set => fields.EntryDate[this] = value; }
    public partial class RowFields { public DateTimeField EntryDate; }

    [DisplayName("Description"), Size(500)]
    public string Description { get => fields.Description[this]; set => fields.Description[this] = value; }
    public partial class RowFields { public StringField Description; }

    [DisplayName("Status"), NotNull, DefaultValue(0)]
    public StockEntryStatus? Status { get => (StockEntryStatus?)fields.Status[this]; set => fields.Status[this] = (short?)value; }
    public partial class RowFields { public Int16Field Status; }

    [DisplayName("Attachments")]
    [MultipleImageUploadEditor(FilenameFormat = "StockEntries/~", AllowNonImage = true, MaxSize = 10485760)]
    public string Attachments { get => fields.Attachments[this]; set => fields.Attachments[this] = value; }
    public partial class RowFields { public StringField Attachments; }

    [DisplayName("Insert Date"), Insertable(false), Updatable(false)]
    public DateTime? InsertDate { get => fields.InsertDate[this]; set => fields.InsertDate[this] = value; }
    public partial class RowFields { public DateTimeField InsertDate; }

    [DisplayName("Insert User"), Insertable(false), Updatable(false)]
    public int? InsertUserId { get => fields.InsertUserId[this]; set => fields.InsertUserId[this] = value; }
    public partial class RowFields { public Int32Field InsertUserId; }

    [DisplayName("Update Date"), Insertable(false), Updatable(false)]
    public DateTime? UpdateDate { get => fields.UpdateDate[this]; set => fields.UpdateDate[this] = value; }
    public partial class RowFields { public DateTimeField UpdateDate; }

    [DisplayName("Update User"), Insertable(false), Updatable(false)]
    public int? UpdateUserId { get => fields.UpdateUserId[this]; set => fields.UpdateUserId[this] = value; }
    public partial class RowFields { public Int32Field UpdateUserId; }

    // Master-Detail için detay listesi
    [DisplayName("Products"), NotMapped]
    [MasterDetailRelation(foreignKey: nameof(StockEntryDetailsRow.StockEntryId))]
    public List<StockEntryDetailsRow> DetailList { get => fields.DetailList[this]; set => fields.DetailList[this] = value; }
    public partial class RowFields { public RowListField<StockEntryDetailsRow> DetailList; }

    #region Foreign Fields

    [DisplayName("Warehouse Name"), Expression($"{jWarehouse}.[Name]")]
    public string WarehouseName { get => fields.WarehouseName[this]; set => fields.WarehouseName[this] = value; }
    public partial class RowFields { public StringField WarehouseName; }

    [DisplayName("Warehouse Code"), Expression($"{jWarehouse}.[Code]")]
    public string WarehouseCode { get => fields.WarehouseCode[this]; set => fields.WarehouseCode[this] = value; }
    public partial class RowFields { public StringField WarehouseCode; }

    #endregion

    public partial class RowFields : RowFieldsBase { }
}
