import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { AuditLogRow } from "./AuditLogRow";

export interface AuditLogColumns {
    Id: Column<AuditLogRow>;
    ActionDate: Column<AuditLogRow>;
    ActionType: Column<AuditLogRow>;
    EntityType: Column<AuditLogRow>;
    EntityName: Column<AuditLogRow>;
    EntityId: Column<AuditLogRow>;
    Username: Column<AuditLogRow>;
    IpAddress: Column<AuditLogRow>;
    ChangedFields: Column<AuditLogRow>;
}

export class AuditLogColumns extends ColumnsBase<AuditLogRow> {
    static readonly columnsKey = 'Administration.AuditLog';
    static readonly Fields = fieldsProxy<AuditLogColumns>();
}