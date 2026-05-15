import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { EmailPriority } from "../_Ext/EmailPriority";
import { EmailQueueRow } from "./EmailQueueRow";

export interface EmailQueueColumns {
    Id: Column<EmailQueueRow>;
    SmtpSettingsName: Column<EmailQueueRow>;
    Subject: Column<EmailQueueRow>;
    ToAddresses: Column<EmailQueueRow>;
    Priority: Column<EmailQueueRow>;
    Status: Column<EmailQueueRow>;
    RetryCount: Column<EmailQueueRow>;
    ScheduledAt: Column<EmailQueueRow>;
    SentAt: Column<EmailQueueRow>;
    InsertDate: Column<EmailQueueRow>;
}

export class EmailQueueColumns extends ColumnsBase<EmailQueueRow> {
    static readonly columnsKey = 'Email.EmailQueue';
    static readonly Fields = fieldsProxy<EmailQueueColumns>();
}

[EmailPriority]; // referenced types