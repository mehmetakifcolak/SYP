import { fieldsProxy } from "@serenity-is/corelib";
import { EmailLogStatus } from "../_Ext/EmailLogStatus";

export interface EmailLogsRow {
    Id?: number;
    EmailQueueId?: number;
    Status?: EmailLogStatus;
    StatusMessage?: string;
    SmtpResponse?: string;
    ToAddress?: string;
    ProcessStartTime?: string;
    ProcessEndTime?: string;
    Duration?: number;
    InsertDate?: string;
    EmailSubject?: string;
}

export abstract class EmailLogsRow {
    static readonly idProperty = 'Id';
    static readonly localTextPrefix = 'Email.EmailLogs';
    static readonly deletePermission = 'Email:EmailQueue:Read';
    static readonly insertPermission = 'Email:EmailQueue:Read';
    static readonly readPermission = 'Email:EmailQueue:Read';
    static readonly updatePermission = 'Email:EmailQueue:Read';

    static readonly Fields = fieldsProxy<EmailLogsRow>();
}