using Microsoft.AspNetCore.Mvc;
using Serenity.Reporting;
using Serenity.Services;
using System.Data;
using System.Globalization;
using System.Net;
using System.Net.Mail;

namespace SYP.Email.Endpoints;

[Route("Services/Email/SmtpSettings/[action]")]
[ConnectionKey(typeof(SmtpSettingsRow)), ServiceAuthorize(typeof(SmtpSettingsRow))]
public class SmtpSettingsEndpoint : ServiceEndpoint
{
    [HttpPost, AuthorizeCreate(typeof(SmtpSettingsRow))]
    public SaveResponse Create(IUnitOfWork uow, SaveRequest<SmtpSettingsRow> request,
        [FromServices] ISmtpSettingsSaveHandler handler)
    {
        return handler.Create(uow, request);
    }

    [HttpPost, AuthorizeUpdate(typeof(SmtpSettingsRow))]
    public SaveResponse Update(IUnitOfWork uow, SaveRequest<SmtpSettingsRow> request,
        [FromServices] ISmtpSettingsSaveHandler handler)
    {
        return handler.Update(uow, request);
    }

    [HttpPost, AuthorizeDelete(typeof(SmtpSettingsRow))]
    public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
        [FromServices] ISmtpSettingsDeleteHandler handler)
    {
        return handler.Delete(uow, request);
    }

    [HttpPost]
    public RetrieveResponse<SmtpSettingsRow> Retrieve(IDbConnection connection, RetrieveRequest request,
        [FromServices] ISmtpSettingsRetrieveHandler handler)
    {
        return handler.Retrieve(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(SmtpSettingsRow))]
    public ListResponse<SmtpSettingsRow> List(IDbConnection connection, ListRequest request,
        [FromServices] ISmtpSettingsListHandler handler)
    {
        return handler.List(connection, request);
    }

    [HttpPost, AuthorizeList(typeof(SmtpSettingsRow))]
    public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
        [FromServices] ISmtpSettingsListHandler handler,
        [FromServices] IExcelExporter exporter)
    {
        var data = List(connection, request, handler).Entities;
        var bytes = exporter.Export(data, typeof(Columns.SmtpSettingsColumns), request.ExportColumns);
        return ExcelContentResult.Create(bytes, "SmtpSettings_" +
            DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
    }

    [HttpPost, AuthorizeUpdate(typeof(SmtpSettingsRow))]
    public async Task<TestEmailResponse> TestEmail(IDbConnection connection, TestEmailRequest request)
    {
        var response = new TestEmailResponse();

        try
        {
            // SMTP ayarlarını al
            var smtpSettings = connection.TryById<SmtpSettingsRow>(request.SmtpSettingsId);
            if (smtpSettings == null)
            {
                response.Success = false;
                response.Message = "SMTP ayarı bulunamadı!";
                return response;
            }

            using var client = new SmtpClient(smtpSettings.Host, smtpSettings.Port ?? 587);
            client.EnableSsl = smtpSettings.UseSsl ?? true;
            client.Timeout = 30000; // 30 saniye timeout

            if (!string.IsNullOrEmpty(smtpSettings.Username))
            {
                client.Credentials = new NetworkCredential(
                    smtpSettings.Username,
                    smtpSettings.Password);
            }

            using var message = new MailMessage();
            message.From = new MailAddress(
                smtpSettings.FromAddress!,
                smtpSettings.FromName ?? "Test Mail");
            message.To.Add(request.ToEmail);
            message.Subject = "SMTP Test - " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            message.Body = $@"
<html>
<body style='font-family: Arial, sans-serif;'>
    <h2>SMTP Bağlantı Testi Başarılı!</h2>
    <p>Bu email, SMTP ayarlarınızın doğru çalıştığını doğrulamak için gönderilmiştir.</p>
    <hr/>
    <p><strong>SMTP Sunucu:</strong> {smtpSettings.Host}:{smtpSettings.Port}</p>
    <p><strong>SSL:</strong> {(smtpSettings.UseSsl == true ? "Evet" : "Hayır")}</p>
    <p><strong>Gönderen:</strong> {smtpSettings.FromAddress}</p>
    <p><strong>Tarih:</strong> {DateTime.Now:dd.MM.yyyy HH:mm:ss}</p>
</body>
</html>";
            message.IsBodyHtml = true;

            await client.SendMailAsync(message);

            response.Success = true;
            response.Message = $"Test emaili başarıyla gönderildi: {request.ToEmail}";
        }
        catch (SmtpException ex)
        {
            response.Success = false;
            response.Message = $"SMTP Hatası: {ex.Message}";
            if (ex.InnerException != null)
                response.Message += $" - {ex.InnerException.Message}";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"Hata: {ex.Message}";
        }

        return response;
    }
}

public class TestEmailRequest : ServiceRequest
{
    public int SmtpSettingsId { get; set; }
    public string ToEmail { get; set; } = "";
}

public class TestEmailResponse : ServiceResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = "";
}
