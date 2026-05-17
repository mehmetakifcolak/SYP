
namespace SYP.Warehouse;

[ConnectionKey("Default"), Module("Warehouse"), TableName("StockExits")]
[DisplayName("Stock Exits"), InstanceName("Stock Exit")]
[NavigationPermission("Warehouse:StockExits:Navigation")]
[ReadPermission("Warehouse:StockExits:Read")]
[InsertPermission("Warehouse:StockExits:Insert")]
[UpdatePermission("Warehouse:StockExits:Update")]
[DeletePermission("Warehouse:StockExits:Delete")]
public sealed class StockExitsRow : Row<StockExitsRow.RowFields>, IIdRow, INameRow, SYP.Administration.IAuditedRow
{
    const string jWarehouse = nameof(jWarehouse);

    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }

    [DisplayName("Exit No"), Size(50), NotNull, QuickSearch, NameProperty]
    public string ExitNo { get => fields.ExitNo[this]; set => fields.ExitNo[this] = value; }
    public partial class RowFields { public StringField ExitNo; }

    [DisplayName("Warehouse"), NotNull, ForeignKey(typeof(WarehousesRow)), LeftJoin(jWarehouse)]
    [LookupEditor(typeof(WarehousesRow))]
    public int? WarehouseId { get => fields.WarehouseId[this]; set => fields.WarehouseId[this] = value; }
    public partial class RowFields { public Int32Field WarehouseId; }

    [DisplayName("Exit Date"), NotNull]
    public DateTime? ExitDate { get => fields.ExitDate[this]; set => fields.ExitDate[this] = value; }
    public partial class RowFields { public DateTimeField ExitDate; }

    [DisplayName("Description"), Size(500)]
    public string Description { get => fields.Description[this]; set => fields.Description[this] = value; }
    public partial class RowFields { public StringField Description; }

    [DisplayName("Status"), NotNull, DefaultValue(0)]
    public StockExitStatus? Status { get => (StockExitStatus?)fields.Status[this]; set => fields.Status[this] = (short?)value; }
    public partial class RowFields { public Int16Field Status; }

    [DisplayName("Attachments")]
    [MultipleImageUploadEditor(FilenameFormat = "StockExits/~", AllowNonImage = true, MaxSize = 10485760)]
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
    [MasterDetailRelation(foreignKey: nameof(StockExitDetailsRow.StockExitId))]
    public List<StockExitDetailsRow> DetailList { get => fields.DetailList[this]; set => fields.DetailList[this] = value; }
    public partial class RowFields { public RowListField<StockExitDetailsRow> DetailList; }

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
