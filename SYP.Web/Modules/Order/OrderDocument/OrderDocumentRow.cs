namespace SYP.Order;

[ConnectionKey("Default"), Module("Order"), TableName("OrderDocuments")]
[DisplayName("Order Document"), InstanceName("Order Document")]
[NavigationPermission("Order:OrderDocument:Navigation")]
[ReadPermission("Order:OrderDocument:Read")]
[InsertPermission("Order:OrderDocument:Insert")]
[UpdatePermission("Order:OrderDocument:Update")]
[DeletePermission("Order:OrderDocument:Delete")]
[ServiceLookupPermission("Order:OrderDocument:Lookup")]
public sealed class OrderDocumentRow : Row<OrderDocumentRow.RowFields>, IIdRow, INameRow
{
    const string jOrder = nameof(jOrder);
    const string jUploadedByUser = nameof(jUploadedByUser);

    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }
    
    [DisplayName("Order"), NotNull, ForeignKey(typeof(OrderRow)), LeftJoin(jOrder), TextualField(nameof(OrderNumber))]
    [ServiceLookupEditor(typeof(OrderRow))]
    public int? OrderId { get => fields.OrderId[this]; set => fields.OrderId[this] = value; }
    public partial class RowFields { public Int32Field OrderId; }
    
    [DisplayName("Document Type"), NotNull]
    public int? DocumentType { get => fields.DocumentType[this]; set => fields.DocumentType[this] = value; }
    public partial class RowFields { public Int32Field DocumentType; }
    
    [DisplayName("File Name"), Size(255), NotNull, QuickSearch, NameProperty]
    public string FileName { get => fields.FileName[this]; set => fields.FileName[this] = value; }
    public partial class RowFields { public StringField FileName; }
    
    [DisplayName("File Path"), Size(500), NotNull]
    public string FilePath { get => fields.FilePath[this]; set => fields.FilePath[this] = value; }
    public partial class RowFields { public StringField FilePath; }
    
    [DisplayName("File Size"), NotNull]
    public long? FileSize { get => fields.FileSize[this]; set => fields.FileSize[this] = value; }
    public partial class RowFields { public Int64Field FileSize; }
    
    [DisplayName("Mime Type"), Size(100)]
    public string MimeType { get => fields.MimeType[this]; set => fields.MimeType[this] = value; }
    public partial class RowFields { public StringField MimeType; }
    
    [DisplayName("Uploaded By User"), NotNull, ForeignKey(typeof(Administration.UserRow)), LeftJoin(jUploadedByUser)]
    [TextualField(nameof(UploadedByUserUsername)), LookupEditor(typeof(Administration.UserRow), Async = true)]
    public int? UploadedByUserId { get => fields.UploadedByUserId[this]; set => fields.UploadedByUserId[this] = value; }
    public partial class RowFields { public Int32Field UploadedByUserId; }
    
    [DisplayName("Upload Date"), NotNull]
    public DateTime? UploadDate { get => fields.UploadDate[this]; set => fields.UploadDate[this] = value; }
    public partial class RowFields { public DateTimeField UploadDate; }
    
    [DisplayName("Is Active"), NotNull]
    public bool? IsActive { get => fields.IsActive[this]; set => fields.IsActive[this] = value; }
    public partial class RowFields { public BooleanField IsActive; }
    
    [DisplayName("Notes"), Size(500)]
    public string Notes { get => fields.Notes[this]; set => fields.Notes[this] = value; }
    public partial class RowFields { public StringField Notes; }
    
    #region Foreign Fields

    [DisplayName("Order Order Number"), Origin(jOrder, nameof(OrderRow.OrderNumber)), ReadOnly(true)]
    public string OrderNumber { get => fields.OrderNumber[this]; set => fields.OrderNumber[this] = value; }
    public partial class RowFields { public StringField OrderNumber; }

    [DisplayName("Uploaded By User Username"), Origin(jUploadedByUser, nameof(Administration.UserRow.Username)), ReadOnly(true)]
    public string UploadedByUserUsername { get => fields.UploadedByUserUsername[this]; set => fields.UploadedByUserUsername[this] = value; }
    public partial class RowFields { public StringField UploadedByUserUsername; }

    #endregion Foreign Fields

    public partial class RowFields : RowFieldsBase { }
}