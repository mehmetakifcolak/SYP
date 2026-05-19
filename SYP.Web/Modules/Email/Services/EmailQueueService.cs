using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serenity.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.Json;

namespace SYP.Email.Services;

public class EmailQueueService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<EmailQueueService> _logger;
    private readonly IConfiguration _configuration;
    private readonly TimeSpan _pollingInterval;
    private readonly int _batchSize;

    public EmailQueueService(
        IServiceProvider serviceProvider,
        ILogger<EmailQueueService> logger,
        IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _configuration = configuration;
        _pollingInterval = TimeSpan.FromSeconds(
            configuration.GetValue("Email:PollingIntervalSeconds", 30));
        _batchSize = configuration.GetValue("Email:BatchSize", 10);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Email Queue Service starting...");

        try
        {
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessQueueAsync(stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing email queue");
                }

                await Task.Delay(_pollingInterval, stoppingToken);
            }
        }
        catch (OperationCanceledException)
        {
            // Normal shutdown
        }

        _logger.LogInformation("Email Queue Service stopping...");
    }

    private async Task ProcessQueueAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var sqlConnections = scope.ServiceProvider.GetRequiredService<ISqlConnections>();

        using var connection = sqlConnections.NewByKey("Default");

        var pendingEmails = GetPendingEmails(connection);

        if (pendingEmails.Count == 0)
            return;

        _logger.LogDebug("Processing {Count} emails from queue", pendingEmails.Count);

        foreach (var email in pendingEmails)
        {
            if (stoppingToken.IsCancellationRequested)
                break;

            await ProcessEmailAsync(connection, email);
        }
    }

    private List<EmailQueueRow> GetPendingEmails(IDbConnection connection)
    {
        var now = DateTime.Now;
        var fld = EmailQueueRow.Fields;

        return connection.List<EmailQueueRow>(q => q
            .Select(fld.Id, fld.SmtpSettingsId, fld.ToAddresses, fld.CcAddresses,
                    fld.BccAddresses, fld.FromAddress, fld.FromName, fld.ReplyToAddress,
                    fld.Subject, fld.Body, fld.BodyText, fld.Priority, fld.RetryCount)
            .Where(
                // Pending ve zamanı gelmiş
                (new Criteria(fld.Status) == (int)EmailQueueStatus.Pending &
                 (new Criteria(fld.ScheduledAt).IsNull() | new Criteria(fld.ScheduledAt) <= now)) |
                // Scheduled ve zamanı gelmiş
                (new Criteria(fld.Status) == (int)EmailQueueStatus.Scheduled &
                 new Criteria(fld.ScheduledAt) <= now) |
                // Retrying ve tekrar zamanı gelmiş
                (new Criteria(fld.Status) == (int)EmailQueueStatus.Retrying &
                 new Criteria(fld.NextRetryAt) <= now))
            .OrderBy(fld.Priority)
            .OrderBy(fld.InsertDate)
            .Take(_batchSize));
    }

    private async Task ProcessEmailAsync(IDbConnection connection, EmailQueueRow email)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        try
        {
            // Status'u Processing yap
            connection.UpdateById(new EmailQueueRow
            {
                Id = email.Id,
                Status = EmailQueueStatus.Processing,
                ProcessedAt = DateTime.Now
            });

            AddLog(connection, email.Id!.Value, EmailLogStatus.Processing, "Email işleniyor...");

            // SMTP ayarlarını al
            var smtpSettings = GetSmtpSettings(connection, email.SmtpSettingsId);

            // Ekleri al
            var attachments = GetAttachments(connection, email.Id.Value);

            // Email gönder
            await SendEmailAsync(smtpSettings, email, attachments);

            stopwatch.Stop();

            // Başarılı
            connection.UpdateById(new EmailQueueRow
            {
                Id = email.Id,
                Status = EmailQueueStatus.Sent,
                SentAt = DateTime.Now,
                ErrorMessage = null
            });

            AddLog(connection, email.Id.Value, EmailLogStatus.Sent,
                $"Email başarıyla gönderildi ({stopwatch.ElapsedMilliseconds}ms)",
                duration: (int)stopwatch.ElapsedMilliseconds);

            _logger.LogInformation("Email {EmailId} sent successfully", email.Id);
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            _logger.LogError(ex, "Failed to send email {EmailId}", email.Id);

            var smtpSettings = GetSmtpSettings(connection, email.SmtpSettingsId);
            var newRetryCount = (email.RetryCount ?? 0) + 1;

            if (newRetryCount < (smtpSettings?.MaxRetryCount ?? 3))
            {
                // Tekrar dene
                connection.UpdateById(new EmailQueueRow
                {
                    Id = email.Id,
                    Status = EmailQueueStatus.Retrying,
                    RetryCount = newRetryCount,
                    NextRetryAt = DateTime.Now.AddMinutes(smtpSettings?.RetryIntervalMinutes ?? 15),
                    ErrorMessage = ex.Message
                });

                AddLog(connection, email.Id!.Value, EmailLogStatus.Failed,
                    $"Gönderim hatası (Deneme {newRetryCount}): {ex.Message}",
                    duration: (int)stopwatch.ElapsedMilliseconds);
            }
            else
            {
                // Maksimum deneme aşıldı
                connection.UpdateById(new EmailQueueRow
                {
                    Id = email.Id,
                    Status = EmailQueueStatus.Failed,
                    RetryCount = newRetryCount,
                    ErrorMessage = ex.Message
                });

                AddLog(connection, email.Id!.Value, EmailLogStatus.Failed,
                    $"Kalıcı hata (Maksimum deneme aşıldı): {ex.Message}",
                    duration: (int)stopwatch.ElapsedMilliseconds);
            }
        }
    }

    private async Task SendEmailAsync(SmtpSettingsRow smtpSettings, EmailQueueRow email, List<EmailAttachmentsRow> attachments)
    {
        if (smtpSettings == null)
            throw new InvalidOperationException("SMTP ayarları bulunamadı!");

        using var client = new SmtpClient(smtpSettings.Host, smtpSettings.Port ?? 587);
        client.EnableSsl = smtpSettings.UseSsl ?? true;

        if (!string.IsNullOrEmpty(smtpSettings.Username))
        {
            client.Credentials = new NetworkCredential(
                smtpSettings.Username,
                smtpSettings.Password); // TODO: Şifre decryption
        }

        using var message = new MailMessage();

        // From
        message.From = new MailAddress(
            email.FromAddress ?? smtpSettings.FromAddress!,
            email.FromName ?? smtpSettings.FromName);

        // To, Cc, Bcc
        var toAddresses = JsonSerializer.Deserialize<List<string>>(email.ToAddresses!) ?? new List<string>();
        foreach (var to in toAddresses)
            message.To.Add(to);

        if (!string.IsNullOrEmpty(email.CcAddresses))
        {
            var ccAddresses = JsonSerializer.Deserialize<List<string>>(email.CcAddresses) ?? new List<string>();
            foreach (var cc in ccAddresses)
                message.CC.Add(cc);
        }

        if (!string.IsNullOrEmpty(email.BccAddresses))
        {
            var bccAddresses = JsonSerializer.Deserialize<List<string>>(email.BccAddresses) ?? new List<string>();
            foreach (var bcc in bccAddresses)
                message.Bcc.Add(bcc);
        }

        // Reply-To
        if (!string.IsNullOrEmpty(email.ReplyToAddress))
            message.ReplyToList.Add(email.ReplyToAddress);

        // Subject & Body
        message.Subject = email.Subject;
        message.Body = email.Body;
        message.IsBodyHtml = true;

        // Plain text alternate view
        if (!string.IsNullOrEmpty(email.BodyText))
        {
            var plainView = AlternateView.CreateAlternateViewFromString(
                email.BodyText, null, "text/plain");
            message.AlternateViews.Add(plainView);
        }

        // Attachments
        foreach (var att in attachments)
        {
            Attachment attachment = null;

            if (att.FileContent != null)
            {
                attachment = new Attachment(
                    new MemoryStream(att.FileContent),
                    att.FileName,
                    att.ContentType);
            }
            else if (!string.IsNullOrEmpty(att.FilePath) && File.Exists(att.FilePath))
            {
                attachment = new Attachment(att.FilePath);
            }

            if (attachment != null)
            {
                if (att.IsInline == true && !string.IsNullOrEmpty(att.ContentId))
                {
                    attachment.ContentDisposition!.Inline = true;
                    attachment.ContentId = att.ContentId;
                }

                message.Attachments.Add(attachment);
            }
        }

        await client.SendMailAsync(message);
    }

    private SmtpSettingsRow GetSmtpSettings(IDbConnection connection, int? smtpSettingsId)
    {
        if (smtpSettingsId.HasValue)
        {
            return connection.TryById<SmtpSettingsRow>(smtpSettingsId.Value);
        }

        var fld = SmtpSettingsRow.Fields;
        return connection.TryFirst<SmtpSettingsRow>(q => q
            .SelectTableFields()
            .Where(new Criteria(fld.IsDefault) == 1 & new Criteria(fld.IsActive) == 1));
    }

    private List<EmailAttachmentsRow> GetAttachments(IDbConnection connection, long emailQueueId)
    {
        var fld = EmailAttachmentsRow.Fields;
        return connection.List<EmailAttachmentsRow>(q => q
            .SelectTableFields()
            .Where(new Criteria(fld.EmailQueueId) == emailQueueId));
    }

    private void AddLog(IDbConnection connection, long emailQueueId,
        EmailLogStatus status, string message, int? duration = null)
    {
        connection.Insert(new EmailLogsRow
        {
            EmailQueueId = emailQueueId,
            Status = status,
            StatusMessage = message,
            Duration = duration,
            ProcessStartTime = status == EmailLogStatus.Processing ? DateTime.Now : null,
            ProcessEndTime = status != EmailLogStatus.Processing ? DateTime.Now : null,
            InsertDate = DateTime.Now
        });
    }
}
