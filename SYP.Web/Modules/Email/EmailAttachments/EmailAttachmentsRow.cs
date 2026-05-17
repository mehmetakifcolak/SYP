namespace SYP.Email;

[ConnectionKey("Default"), Module("Email"), TableName("EmailAttachments")]
[DisplayName("Email Attachments"), InstanceName("Email Attachment")]
[NavigationPermission("Email:EmailAttachments:Navigation")]
[ReadPermission("Email:EmailQueue:Read")]
[InsertPermission("Email:EmailQueue:Insert")]
[UpdatePermission("Email:EmailQueue:Update")]
[DeletePermission("Email:EmailQueue:Delete")]
public sealed class EmailAttachmentsRow : Row<EmailAttachmentsRow.RowFields>, IIdRow, INameRow
{
    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }

    [DisplayName("Email"), NotNull, ForeignKey(typeof(EmailQueueRow)), LeftJoin("jEmailQueue")]
    public long? EmailQueueId { get => fields.EmailQueueId[this]; set => fields.EmailQueueId[this] = value; }
    public partial class RowFields { public Int64Field EmailQueueId; }

    [DisplayName("File Name"), Size(255), NotNull, QuickSearch, NameProperty]
    public string FileName { get => fields.FileName[this]; set => fields.FileName[this] = value; }
    public partial class RowFields { public StringField FileName; }

    [DisplayName("Content Type"), Size(100), NotNull]
    public string ContentType { get => fields.ContentType[this]; set => fields.ContentType[this] = value; }
    public partial class RowFields { public StringField ContentType; }

    [DisplayName("File Path"), Size(500)]
    public string FilePath { get => fields.FilePath[this]; set => fields.FilePath[this] = value; }
    public partial class RowFields { public StringField FilePath; }

    [DisplayName("File Content")]
    public byte[] FileContent { get => fields.FileContent[this]; set => fields.FileContent[this] = value; }
    public partial class RowFields { public ByteArrayField FileContent; }

    [DisplayName("File Size"), NotNull]
    public long? FileSize { get => fields.FileSize[this]; set => fields.FileSize[this] = value; }
    public partial class RowFields { public Int64Field FileSize; }

    [DisplayName("Inline"), NotNull, DefaultValue(false)]
    public bool? IsInline { get => fields.IsInline[this]; set => fields.IsInline[this] = value; }
    public partial class RowFields { public BooleanField IsInline; }

    [DisplayName("Content ID"), Size(100)]
    public string ContentId { get => fields.ContentId[this]; set => fields.ContentId[this] = value; }
    public partial class RowFields { public StringField ContentId; }

    [DisplayName("Insert Date"), Insertable(false), Updatable(false)]
    public DateTime? InsertDate { get => fields.InsertDate[this]; set => fields.InsertDate[this] = value; }
    public partial class RowFields { public DateTimeField InsertDate; }

    public partial class RowFields : RowFieldsBase { }
}
