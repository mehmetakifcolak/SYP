namespace SYP.Email.Columns;

[ColumnsScript("Email.SmtpSettings")]
[BasedOnRow(typeof(SmtpSettingsRow), CheckNames = true)]
public class SmtpSettingsColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, Width(80)]
    public int Id { get; set; }

    [EditLink, Width(200)]
    public string Name { get; set; }

    [Width(200)]
    public string Host { get; set; }

    [Width(80)]
    public int Port { get; set; }

    [Width(80)]
    public bool UseSsl { get; set; }

    [Width(200)]
    public string FromAddress { get; set; }

    [Width(80)]
    public bool IsDefault { get; set; }

    [Width(80)]
    public bool IsActive { get; set; }
}
