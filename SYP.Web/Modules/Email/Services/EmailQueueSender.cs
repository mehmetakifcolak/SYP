using Microsoft.Extensions.Logging;
using Serenity.Data;
using Serenity.Web;
using SYP.Administration;
using _Ext;
using System.Text.Json;

namespace SYP.Email.Services;

public class EmailQueueSender : IEmailQueueSender
{
    private readonly ISqlConnections _sqlConnections;
    private readonly IUserAccessor _userAccessor;
    private readonly ILogger<EmailQueueSender> _logger;

    public EmailQueueSender(
        ISqlConnections sqlConnections,
        IUserAccessor userAccessor,
        ILogger<EmailQueueSender> logger)
    {
        _sqlConnections = sqlConnections;
        _userAccessor = userAccessor;
        _logger = logger;
    }

    public async Task<long> QueueEmailAsync(QueueEmailRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(request.To);
        ArgumentException.ThrowIfNullOrEmpty(request.Subject);
        ArgumentException.ThrowIfNullOrEmpty(request.Body);

        using var connection = _sqlConnections.NewByKey("Default");

        var smtpSettingsId = request.SmtpSettingsId ?? await GetDefaultSmtpSettingsIdAsync(connection);

        var emailQueue = new EmailQueueRow
        {
            SmtpSettingsId = smtpSettingsId,
            ToAddresses = JsonSerializer.Serialize(request.To),
            CcAddresses = request.Cc != null ? JsonSerializer.Serialize(request.Cc) : null,
            BccAddresses = request.Bcc != null ? JsonSerializer.Serialize(request.Bcc) : null,
            Subject = request.Subject,
            Body = request.Body,
            BodyText = request.BodyText,
            FromAddress = request.FromAddress,
            FromName = request.FromName,
            ReplyToAddress = request.ReplyTo,
            Priority = request.Priority,
            Status = EmailQueueStatus.Pending,
            ReferenceType = request.ReferenceType,
            ReferenceId = request.ReferenceId,
            RetryCount = 0,
            InsertDate = DateTime.Now,
            InsertUserId = GetCurrentUserId()
        };

        var emailId = Convert.ToInt64(connection.InsertAndGetID(emailQueue));

        // Ekleri kaydet
        if (request.Attachments?.Count > 0)
        {
            foreach (var attachment in request.Attachments)
            {
                var emailAttachment = new EmailAttachmentsRow
                {
                    EmailQueueId = emailId,
                    FileName = attachment.FileName,
                    ContentType = attachment.ContentType,
                    FileContent = attachment.Content,
                    FilePath = attachment.FilePath,
                    FileSize = attachment.Content?.Length ?? 0,
                    IsInline = attachment.IsInline,
                    ContentId = attachment.ContentId,
                    InsertDate = DateTime.Now
                };
                connection.Insert(emailAttachment);
            }
        }

        // Log ekle
        connection.Insert(new EmailLogsRow
        {
            EmailQueueId = emailId,
            Status = EmailLogStatus.Queued,
            StatusMessage = "Email kuyruğa eklendi",
            InsertDate = DateTime.Now
        });

        _logger.LogInformation("Email queued with ID: {EmailId}", emailId);
        return emailId;
    }

    public async Task<long> QueueTemplateEmailAsync(QueueTemplateEmailRequest request)
    {
        using var connection = _sqlConnections.NewByKey("Default");

        var fld = EmailTemplatesRow.Fields;
        var template = connection.TryFirst<EmailTemplatesRow>(q => q
            .Select(fld.Id, fld.Subject, fld.Body, fld.BodyText)
            .Where(
                new Criteria(fld.TemplateKey) == request.TemplateKey &
                new Criteria(fld.IsActive) == 1));

        if (template == null)
            throw new ValidationError($"Email şablonu bulunamadı: {request.TemplateKey}");

        // Placeholder'ları değiştir
        var subject = ProcessTemplate(template.Subject, request.TemplateData);
        var body = ProcessTemplate(template.Body, request.TemplateData);
        var bodyText = template.BodyText != null
            ? ProcessTemplate(template.BodyText, request.TemplateData)
            : null;

        return await QueueEmailAsync(new QueueEmailRequest
        {
            To = request.To,
            Cc = request.Cc,
            Bcc = request.Bcc,
            Subject = subject,
            Body = body,
            BodyText = bodyText,
            Priority = request.Priority,
            SmtpSettingsId = request.SmtpSettingsId,
            ReferenceType = request.ReferenceType,
            ReferenceId = request.ReferenceId,
            Attachments = request.Attachments
        });
    }

    public async Task<long> QueueScheduledEmailAsync(QueueEmailRequest request, DateTime scheduledAt)
    {
        var emailId = await QueueEmailAsync(request);

        using var connection = _sqlConnections.NewByKey("Default");
        connection.UpdateById(new EmailQueueRow
        {
            Id = emailId,
            Status = EmailQueueStatus.Scheduled,
            ScheduledAt = scheduledAt
        });

        return emailId;
    }

    public Task<bool> CancelEmailAsync(long emailQueueId)
    {
        using var connection = _sqlConnections.NewByKey("Default");

        var row = connection.TryById<EmailQueueRow>(emailQueueId);
        if (row == null)
            return Task.FromResult(false);

        if (row.Status == EmailQueueStatus.Sent || row.Status == EmailQueueStatus.Processing)
            return Task.FromResult(false);

        connection.UpdateById(new EmailQueueRow
        {
            Id = emailQueueId,
            Status = EmailQueueStatus.Cancelled
        });

        return Task.FromResult(true);
    }

    public Task<bool> ResendEmailAsync(long emailQueueId)
    {
        using var connection = _sqlConnections.NewByKey("Default");

        var row = connection.TryById<EmailQueueRow>(emailQueueId);
        if (row == null)
            return Task.FromResult(false);

        connection.UpdateById(new EmailQueueRow
        {
            Id = emailQueueId,
            Status = EmailQueueStatus.Pending,
            RetryCount = 0,
            ErrorMessage = null,
            NextRetryAt = null
        });

        return Task.FromResult(true);
    }

    private string ProcessTemplate(string? template, Dictionary<string, object?>? data)
    {
        if (string.IsNullOrEmpty(template) || data == null)
            return template ?? "";

        foreach (var kvp in data)
        {
            template = template.Replace($"{{{{{kvp.Key}}}}}", kvp.Value?.ToString() ?? "");
        }
        return template;
    }

    private Task<int> GetDefaultSmtpSettingsIdAsync(IDbConnection connection)
    {
        var fld = SmtpSettingsRow.Fields;
        var defaultSmtp = connection.TryFirst<SmtpSettingsRow>(q => q
            .Select(fld.Id)
            .Where(
                new Criteria(fld.IsDefault) == 1 &
                new Criteria(fld.IsActive) == 1));

        if (defaultSmtp == null)
            throw new ValidationError("Varsayılan SMTP ayarı bulunamadı!");

        return Task.FromResult(defaultSmtp.Id!.Value);
    }

    private int GetCurrentUserId()
    {
        var identifier = _userAccessor.User?.GetIdentifier();
        if (string.IsNullOrEmpty(identifier))
            return 1;

        if (int.TryParse(identifier, out var userId))
            return userId;

        // Identifier username ise veritabanindan UserId'yi bul
        using var connection = _sqlConnections.NewByKey("Default");
        var user = connection.TryFirst<UserRow>(q => q
            .Select(UserRow.Fields.UserId)
            .Where(UserRow.Fields.Username == identifier));

        return user?.UserId ?? 1;
    }
}
