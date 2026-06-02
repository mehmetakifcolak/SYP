using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SYP.Order;

/// <summary>
/// Sipariş durum değişikliği bildirim e-postaları için şık, e-posta istemcisi
/// uyumlu (inline stil, tablo tabanlı) HTML gövde üretir.
/// Saf string üretimi yapar; veritabanı erişimi içermez.
/// </summary>
public static class OrderStatusEmailBuilder
{
    public sealed class Line
    {
        public int No { get; set; }
        public string Product { get; set; }
        public string Quantity { get; set; }
        public string UnitPrice { get; set; }
        public string LineTotal { get; set; }
    }

    public sealed class Model
    {
        public string RecipientName { get; set; }
        public string IntroHtml { get; set; }
        public string StatusLabel { get; set; }
        public string AccentColor { get; set; } = "#7c3aed";
        public string OrderNumber { get; set; }
        public string BayiName { get; set; }
        public string TemsilciName { get; set; }
        public string OrderDate { get; set; }
        public List<Line> Lines { get; set; } = new();
        public string DiscountTotal { get; set; }
        public string NetTotal { get; set; }
        public string Reason { get; set; }
        public string ChangedBy { get; set; }
        public string OrderLink { get; set; }
    }

    private static string Enc(string s) => WebUtility.HtmlEncode(s ?? "");

    public static string BuildSubject(string statusLabel, string orderNumber)
        => $"{statusLabel} — Sipariş {orderNumber}";

    public static string BuildPlainText(Model m)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Sayın {m.RecipientName},");
        sb.AppendLine();
        sb.AppendLine($"{m.OrderNumber} numaralı siparişin durumu: {m.StatusLabel}");
        sb.AppendLine();
        sb.AppendLine("Sipariş Kalemleri:");
        foreach (var l in m.Lines)
            sb.AppendLine($"  {l.No}. {l.Product}  x {l.Quantity}  =  {l.LineTotal}");
        sb.AppendLine();
        if (!string.IsNullOrWhiteSpace(m.DiscountTotal))
            sb.AppendLine($"İndirim: -{m.DiscountTotal}");
        sb.AppendLine($"Net Toplam: {m.NetTotal}");
        if (!string.IsNullOrWhiteSpace(m.Reason))
            sb.AppendLine($"Açıklama: {m.Reason}");
        sb.AppendLine();
        sb.AppendLine($"Sipariş detayı: {m.OrderLink}");
        return sb.ToString();
    }

    public static string BuildBody(Model m)
    {
        var rows = new StringBuilder();
        foreach (var l in m.Lines)
        {
            rows.Append($@"
              <tr>
                <td style=""padding:10px 8px;border-bottom:1px solid #eef0f4;color:#6b7280;font-size:13px;text-align:center;"">{l.No}</td>
                <td style=""padding:10px 8px;border-bottom:1px solid #eef0f4;color:#111827;font-size:13px;font-weight:600;"">{Enc(l.Product)}</td>
                <td style=""padding:10px 8px;border-bottom:1px solid #eef0f4;color:#374151;font-size:13px;text-align:center;white-space:nowrap;"">{Enc(l.Quantity)}</td>
                <td style=""padding:10px 8px;border-bottom:1px solid #eef0f4;color:#6b7280;font-size:13px;text-align:right;white-space:nowrap;"">{Enc(l.UnitPrice)}</td>
                <td style=""padding:10px 8px;border-bottom:1px solid #eef0f4;color:#111827;font-size:13px;font-weight:600;text-align:right;white-space:nowrap;"">{Enc(l.LineTotal)}</td>
              </tr>");
        }

        if (m.Lines.Count == 0)
            rows.Append(@"
              <tr><td colspan=""5"" style=""padding:16px;text-align:center;color:#9ca3af;font-size:13px;"">Sipariş kalemi bulunmuyor.</td></tr>");

        var discountRow = string.IsNullOrWhiteSpace(m.DiscountTotal) ? "" : $@"
              <tr>
                <td colspan=""4"" style=""padding:8px;text-align:right;color:#6b7280;font-size:13px;"">İndirim</td>
                <td style=""padding:8px;text-align:right;color:#dc2626;font-size:13px;font-weight:600;white-space:nowrap;"">- {Enc(m.DiscountTotal)}</td>
              </tr>";

        var reasonBlock = string.IsNullOrWhiteSpace(m.Reason) ? "" : $@"
        <tr><td style=""padding:4px 28px 8px;"">
          <div style=""background:#fff7ed;border:1px solid #fed7aa;border-radius:8px;padding:12px 14px;color:#9a3412;font-size:13px;line-height:1.5;"">
            <strong>Açıklama:</strong> {Enc(m.Reason)}
          </div>
        </td></tr>";

        var changedByBlock = string.IsNullOrWhiteSpace(m.ChangedBy) ? "" :
            $@"<span style=""color:#9ca3af;"">İşlemi yapan: <strong>{Enc(m.ChangedBy)}</strong></span><br>";

        return $@"<!DOCTYPE html>
<html lang=""tr"">
<head><meta charset=""utf-8""><meta name=""viewport"" content=""width=device-width,initial-scale=1""></head>
<body style=""margin:0;padding:0;background:#f1f5f9;font-family:Segoe UI,Roboto,Helvetica,Arial,sans-serif;"">
  <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background:#f1f5f9;padding:24px 12px;"">
    <tr><td align=""center"">
      <table role=""presentation"" width=""600"" cellpadding=""0"" cellspacing=""0"" style=""max-width:600px;width:100%;background:#ffffff;border-radius:14px;overflow:hidden;box-shadow:0 4px 18px rgba(15,23,42,.08);"">

        <tr><td style=""background:{m.AccentColor};padding:22px 28px;"">
          <div style=""color:rgba(255,255,255,.85);font-size:12px;font-weight:600;letter-spacing:.5px;text-transform:uppercase;"">Sipariş Durumu Güncellendi</div>
          <div style=""color:#ffffff;font-size:22px;font-weight:800;margin-top:4px;"">{Enc(m.StatusLabel)}</div>
        </td></tr>

        <tr><td style=""padding:24px 28px 8px;"">
          <div style=""color:#111827;font-size:15px;font-weight:700;margin-bottom:8px;"">Sayın {Enc(m.RecipientName)},</div>
          <div style=""color:#374151;font-size:14px;line-height:1.6;"">{m.IntroHtml}</div>
        </td></tr>

        <tr><td style=""padding:12px 28px 4px;"">
          <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background:#f8fafc;border:1px solid #e5e7eb;border-radius:10px;"">
            <tr>
              <td style=""padding:12px 14px;font-size:12px;color:#6b7280;"">Sipariş No<br><span style=""color:#111827;font-size:14px;font-weight:700;"">{Enc(m.OrderNumber)}</span></td>
              <td style=""padding:12px 14px;font-size:12px;color:#6b7280;"">Tarih<br><span style=""color:#111827;font-size:14px;font-weight:600;"">{Enc(m.OrderDate)}</span></td>
            </tr>
            <tr>
              <td style=""padding:0 14px 12px;font-size:12px;color:#6b7280;"">Bayi<br><span style=""color:#111827;font-size:14px;font-weight:600;"">{Enc(m.BayiName)}</span></td>
              <td style=""padding:0 14px 12px;font-size:12px;color:#6b7280;"">Sorumlu Yönetici<br><span style=""color:#111827;font-size:14px;font-weight:600;"">{Enc(m.TemsilciName)}</span></td>
            </tr>
          </table>
        </td></tr>

        <tr><td style=""padding:18px 28px 4px;"">
          <div style=""color:#111827;font-size:14px;font-weight:700;margin-bottom:8px;"">Sipariş Kalemleri</div>
          <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""border:1px solid #e5e7eb;border-radius:10px;border-collapse:collapse;"">
            <thead><tr style=""background:#f9fafb;"">
              <th style=""padding:9px 8px;font-size:11px;color:#6b7280;text-align:center;text-transform:uppercase;letter-spacing:.4px;"">#</th>
              <th style=""padding:9px 8px;font-size:11px;color:#6b7280;text-align:left;text-transform:uppercase;letter-spacing:.4px;"">Ürün</th>
              <th style=""padding:9px 8px;font-size:11px;color:#6b7280;text-align:center;text-transform:uppercase;letter-spacing:.4px;"">Miktar</th>
              <th style=""padding:9px 8px;font-size:11px;color:#6b7280;text-align:right;text-transform:uppercase;letter-spacing:.4px;"">Birim Fiyat</th>
              <th style=""padding:9px 8px;font-size:11px;color:#6b7280;text-align:right;text-transform:uppercase;letter-spacing:.4px;"">Tutar</th>
            </tr></thead>
            <tbody>{rows}</tbody>
            <tfoot>{discountRow}
              <tr>
                <td colspan=""4"" style=""padding:12px 8px;text-align:right;color:#111827;font-size:14px;font-weight:700;border-top:2px solid #e5e7eb;"">Net Toplam</td>
                <td style=""padding:12px 8px;text-align:right;color:{m.AccentColor};font-size:16px;font-weight:800;border-top:2px solid #e5e7eb;white-space:nowrap;"">{Enc(m.NetTotal)}</td>
              </tr>
            </tfoot>
          </table>
        </td></tr>
{reasonBlock}
        <tr><td style=""padding:18px 28px 26px;"" align=""center"">
          <a href=""{m.OrderLink}"" style=""display:inline-block;background:{m.AccentColor};color:#ffffff;text-decoration:none;font-size:14px;font-weight:700;padding:12px 28px;border-radius:8px;"">Siparişi Görüntüle</a>
        </td></tr>

        <tr><td style=""padding:16px 28px;background:#f8fafc;border-top:1px solid #eef0f4;font-size:12px;color:#9ca3af;line-height:1.6;"">
          {changedByBlock}Bu e-posta sipariş yönetim sistemi tarafından otomatik olarak gönderilmiştir.
        </td></tr>

      </table>
    </td></tr>
  </table>
</body>
</html>";
    }
}
