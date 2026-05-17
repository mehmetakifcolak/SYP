import { fieldsProxy } from "@serenity-is/corelib";
import { EmailPriority } from "./EmailPriority";
import { EmailQueueStatus } from "./EmailQueueStatus";

export interface EmailQueueRow {
    Id?: number;
    SmtpSettingsId?: number;
    TemplateId?: number;
    ToAddresses?: string;
    CcAddresses?: string;
    BccAddresses?: string;
    FromAddress?: string;
    FromName?: string;
    ReplyToAddress?: string;
    Subject?: string;
    Body?: string;
    BodyText?: string;
    TemplateData?: string;
    Priority?: EmailPriority;
    Status?: EmailQueueStatus;
    ScheduledAt?: string;
    ProcessedAt?: string;
    SentAt?: string;
    ErrorMessage?: string;
    RetryCount?: number;
    NextRetryAt?: string;
    ReferenceType?: string;
    ReferenceId?: string;
    InsertDate?: string;
    InsertUserId?: number;
    SmtpSettingsName?: string;
    TemplateName?: string;
}

export abstract class EmailQueueRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'Subject';
    static readonly localTextPrefix = 'Email.EmailQueue';
    static readonly deletePermission = 'Email:EmailQueue:Delete';
    static readonly insertPermission = 'Email:EmailQueue:Insert';
    static readonly readPermission = 'Email:EmailQueue:Read';
    static readonly updatePermission = 'Email:EmailQueue:Update';

    static readonly Fields = fieldsProxy<EmailQueueRow>();
}