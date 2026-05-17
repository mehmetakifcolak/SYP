import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { EmailLogsRow } from "./EmailLogsRow";
import { EmailLogStatus } from "./EmailLogStatus";

export interface EmailLogsColumns {
    Id: Column<EmailLogsRow>;
    EmailSubject: Column<EmailLogsRow>;
    ToAddress: Column<EmailLogsRow>;
    Status: Column<EmailLogsRow>;
    StatusMessage: Column<EmailLogsRow>;
    Duration: Column<EmailLogsRow>;
    InsertDate: Column<EmailLogsRow>;
}

export class EmailLogsColumns extends ColumnsBase<EmailLogsRow> {
    static readonly columnsKey = 'Email.EmailLogs';
    static readonly Fields = fieldsProxy<EmailLogsColumns>();
}

[EmailLogStatus]; // referenced types