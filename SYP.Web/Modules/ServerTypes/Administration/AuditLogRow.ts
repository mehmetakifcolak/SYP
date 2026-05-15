import { fieldsProxy } from "@serenity-is/corelib";

export interface AuditLogRow {
    Id?: number;
    EntityType?: string;
    EntityId?: string;
    EntityName?: string;
    ActionType?: string;
    ActionDate?: string;
    UserId?: number;
    Username?: string;
    IpAddress?: string;
    OldValues?: string;
    NewValues?: string;
    ChangedFields?: string;
    UserDisplayName?: string;
}

export abstract class AuditLogRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'EntityName';
    static readonly localTextPrefix = 'Administration.AuditLog';
    static readonly deletePermission = 'Administration:AuditLog:Read';
    static readonly insertPermission = 'Administration:AuditLog:Read';
    static readonly readPermission = 'Administration:AuditLog:Read';
    static readonly updatePermission = 'Administration:AuditLog:Read';

    static readonly Fields = fieldsProxy<AuditLogRow>();
}