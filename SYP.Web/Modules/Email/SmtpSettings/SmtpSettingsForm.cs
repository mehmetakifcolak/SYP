namespace SYP.Email.Forms;

[FormScript("Email.SmtpSettings")]
[BasedOnRow(typeof(SmtpSettingsRow), CheckNames = true)]
public class SmtpSettingsForm
{
    [Tab("General")]
    public string Name { get; set; }
    public string Host { get; set; }

    [HalfWidth]
    public int Port { get; set; }

    [HalfWidth]
    public bool UseSsl { get; set; }

    [Tab("Credentials")]
    public string Username { get; set; }

    [PasswordEditor]
    public string Password { get; set; }

    [Tab("Sender Information")]
    [EmailAddressEditor]
    public string FromAddress { get; set; }
    public string FromName { get; set; }

    [Tab("Settings")]
    [HalfWidth]
    public bool IsDefault { get; set; }

    [HalfWidth]
    public bool IsActive { get; set; }

    [HalfWidth]
    public int MaxRetryCount { get; set; }

    [HalfWidth]
    public int RetryIntervalMinutes { get; set; }

    public int DailyLimit { get; set; }
}
