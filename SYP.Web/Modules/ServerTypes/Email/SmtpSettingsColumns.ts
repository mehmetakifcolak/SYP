import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { SmtpSettingsRow } from "./SmtpSettingsRow";

export interface SmtpSettingsColumns {
    Id: Column<SmtpSettingsRow>;
    Name: Column<SmtpSettingsRow>;
    Host: Column<SmtpSettingsRow>;
    Port: Column<SmtpSettingsRow>;
    UseSsl: Column<SmtpSettingsRow>;
    FromAddress: Column<SmtpSettingsRow>;
    IsDefault: Column<SmtpSettingsRow>;
    IsActive: Column<SmtpSettingsRow>;
}

export class SmtpSettingsColumns extends ColumnsBase<SmtpSettingsRow> {
    static readonly columnsKey = 'Email.SmtpSettings';
    static readonly Fields = fieldsProxy<SmtpSettingsColumns>();
}