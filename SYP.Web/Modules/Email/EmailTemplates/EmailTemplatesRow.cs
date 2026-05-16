namespace SYP.Email;

[ConnectionKey("Default"), Module("Email"), TableName("EmailTemplates")]
[DisplayName("Email Şablonları"), InstanceName("Email Şablonu")]
[LookupScript("Email.EmailTemplates", Permission = "Email:EmailTemplates:Read")]
[NavigationPermission("Email:EmailTemplates:Navigation")]
[ReadPermission("Email:EmailTemplates:Read")]
[InsertPermission("Email:EmailTemplates:Insert")]
[UpdatePermission("Email:EmailTemplates:Update")]
[DeletePermission("Email:EmailTemplates:Delete")]
[ServiceLookupPermission("Email:EmailTemplates:Read")]
public sealed class EmailTemplatesRow : Row<EmailTemplatesRow.RowFields>, IIdRow, INameRow, SYP.Administration.IAuditedRow
{
    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }

    [DisplayName("Şablon Anahtarı"), Size(100), NotNull, QuickSearch]
    public string TemplateKey { get => fields.TemplateKey[this]; set => fields.TemplateKey[this] = value; }
    public partial class RowFields { public StringField TemplateKey; }

    [DisplayName("Şablon Adı"), Size(200), NotNull, NameProperty]
    public string Name { get => fields.Name[this]; set => fields.Name[this] = value; }
    public partial class RowFields { public StringField Name; }

    [DisplayName("Konu"), Size(500), NotNull]
    public string Subject { get => fields.Subject[this]; set => fields.Subject[this] = value; }
    public partial class RowFields { public StringField Subject; }

    [DisplayName("İçerik (HTML)"), NotNull]
    public string Body { get => fields.Body[this]; set => fields.Body[this] = value; }
    public partial class RowFields { public StringField Body; }

    [DisplayName("İçerik (Text)")]
    public string BodyText { get => fields.BodyText[this]; set => fields.BodyText[this] = value; }
    public partial class RowFields { public StringField BodyText; }

    [DisplayName("Dil"), Size(10)]
    public string LanguageId { get => fields.LanguageId[this]; set => fields.LanguageId[this] = value; }
    public partial class RowFields { public StringField LanguageId; }

    [DisplayName("Kategori"), Size(100)]
    public string Category { get => fields.Category[this]; set => fields.Category[this] = value; }
    public partial class RowFields { public StringField Category; }

    [DisplayName("Açıklama"), Size(500)]
    public string Description { get => fields.Description[this]; set => fields.Description[this] = value; }
    public partial class RowFields { public StringField Description; }

    [DisplayName("Aktif"), NotNull, DefaultValue(true)]
    public bool? IsActive { get => fields.IsActive[this]; set => fields.IsActive[this] = value; }
    public partial class RowFields { public BooleanField IsActive; }

    [DisplayName("Oluşturma Tarihi"), Insertable(false), Updatable(false)]
    public DateTime? InsertDate { get => fields.InsertDate[this]; set => fields.InsertDate[this] = value; }
    public partial class RowFields { public DateTimeField InsertDate; }

    [DisplayName("Oluşturan"), Insertable(false), Updatable(false)]
    public int? InsertUserId { get => fields.InsertUserId[this]; set => fields.InsertUserId[this] = value; }
    public partial class RowFields { public Int32Field InsertUserId; }

    [DisplayName("Güncelleme Tarihi"), Insertable(false), Updatable(false)]
    public DateTime? UpdateDate { get => fields.UpdateDate[this]; set => fields.UpdateDate[this] = value; }
    public partial class RowFields { public DateTimeField UpdateDate; }

    [DisplayName("Güncelleyen"), Insertable(false), Updatable(false)]
    public int? UpdateUserId { get => fields.UpdateUserId[this]; set => fields.UpdateUserId[this] = value; }
    public partial class RowFields { public Int32Field UpdateUserId; }

    public partial class RowFields : RowFieldsBase { }
}
