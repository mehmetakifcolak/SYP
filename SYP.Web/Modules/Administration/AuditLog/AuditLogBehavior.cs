using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace SYP.Administration;

public class AuditLogBehavior : BaseSaveDeleteBehavior, IImplicitBehavior
{
    private readonly ISqlConnections _sqlConnections;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<AuditLogBehavior> _logger;

    public AuditLogBehavior(
        ISqlConnections sqlConnections,
        IHttpContextAccessor httpContextAccessor,
        ILogger<AuditLogBehavior> logger)
    {
        _sqlConnections = sqlConnections;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public bool ActivateFor(IRow row)
    {
        return row is IAuditedRow;
    }

    public override void OnAudit(ISaveRequestHandler handler)
    {
        if (handler.IsCreate)
        {
            InsertAuditLog(handler.Context, handler.Row, null, "Insert");
        }
        else if (handler.IsUpdate)
        {
            InsertAuditLog(handler.Context, handler.Row, handler.Old, "Update");
        }
    }

    public override void OnAudit(IDeleteRequestHandler handler)
    {
        InsertAuditLog(handler.Context, handler.Row, null, "Delete");
    }

    private void InsertAuditLog(IRequestContext context, IRow row, IRow oldRow, string actionType)
    {
        try
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var entityId = row.IdField?.AsObject(row)?.ToString();
            string entityName = null;

            // QuickSearch attribute'u olan field'i name olarak kullan
            foreach (var field in row.Fields)
            {
                if (field.CustomAttributes?.Any(a => a.GetType().Name == "NamePropertyAttribute") == true)
                {
                    entityName = field.AsObject(row)?.ToString();
                    break;
                }
            }

            // Kullanici bilgilerini al
            int? userId = null;
            string username = null;
            var identifier = context.User?.GetIdentifier();

            if (!string.IsNullOrEmpty(identifier))
            {
                if (int.TryParse(identifier, out var parsedId))
                {
                    userId = parsedId;
                }
                else
                {
                    username = identifier;
                    // Username ile UserId bul
                    using var conn = _sqlConnections.NewByKey("Default");
                    var user = conn.TryFirst<UserRow>(q => q
                        .Select(UserRow.Fields.UserId)
                        .Where(UserRow.Fields.Username == identifier));
                    userId = user?.UserId;
                }
            }

            if (username == null && userId.HasValue)
            {
                using var conn = _sqlConnections.NewByKey("Default");
                var user = conn.TryFirst<UserRow>(q => q
                    .Select(UserRow.Fields.Username)
                    .Where(UserRow.Fields.UserId == userId.Value));
                username = user?.Username;
            }

            // Degisiklikleri hesapla
            var (oldValues, newValues, changedFields) = GetChanges(row, oldRow, actionType);

            // Eger hicbir degisiklik yoksa log ekleme
            if (actionType == "Update" && changedFields.Count == 0)
                return;

            using var connection = _sqlConnections.NewByKey("Default");

            var auditLog = new AuditLogRow
            {
                EntityType = row.Table,
                EntityId = entityId,
                EntityName = entityName,
                ActionType = actionType,
                ActionDate = DateTime.Now,
                UserId = userId,
                Username = username,
                IpAddress = httpContext?.Connection?.RemoteIpAddress?.ToString(),
                OldValues = oldValues,
                NewValues = newValues,
                ChangedFields = string.Join(", ", changedFields)
            };

            connection.Insert(auditLog);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "AuditLog kaydi olusturulurken hata olustu");
        }
    }

    private (string oldValues, string newValues, List<string> changedFields) GetChanges(IRow row, IRow oldRow, string actionType)
    {
        var oldDict = new Dictionary<string, object>();
        var newDict = new Dictionary<string, object>();
        var changedFields = new List<string>();

        foreach (var field in row.EnumerateTableFields())
        {
            // InsertDate, UpdateDate gibi alanlari atla
            if (field.Name.EndsWith("Date") && (field.Name.StartsWith("Insert") || field.Name.StartsWith("Update")))
                continue;
            if (field.Name.EndsWith("UserId") && (field.Name.StartsWith("Insert") || field.Name.StartsWith("Update")))
                continue;

            var newValue = row[field.Name];

            if (actionType == "Insert")
            {
                if (newValue != null)
                {
                    newDict[field.Name] = newValue;
                    changedFields.Add(field.Name);
                }
            }
            else if (actionType == "Delete")
            {
                if (newValue != null)
                {
                    oldDict[field.Name] = newValue;
                    changedFields.Add(field.Name);
                }
            }
            else if (actionType == "Update" && oldRow != null)
            {
                if (row.IsAssigned(field))
                {
                    var oldValue = oldRow[field.Name];

                    if (!Equals(oldValue, newValue))
                    {
                        if (oldValue != null)
                            oldDict[field.Name] = oldValue;
                        if (newValue != null)
                            newDict[field.Name] = newValue;
                        changedFields.Add(field.Name);
                    }
                }
            }
        }

        var oldJson = oldDict.Count > 0 ? JsonSerializer.Serialize(oldDict) : null;
        var newJson = newDict.Count > 0 ? JsonSerializer.Serialize(newDict) : null;

        return (oldJson, newJson, changedFields);
    }
}

/// <summary>
/// Audit log kaydedilecek satirlari isaretlemek icin kullanilir.
/// Row sinifina IAuditedRow interface'ini ekleyin.
/// </summary>
public interface IAuditedRow : IRow, IIdRow
{
}
