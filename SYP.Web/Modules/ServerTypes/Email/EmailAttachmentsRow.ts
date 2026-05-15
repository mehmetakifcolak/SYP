import { fieldsProxy } from "@serenity-is/corelib";

export interface EmailAttachmentsRow {
    Id?: number;
    EmailQueueId?: number;
    FileName?: string;
    ContentType?: string;
    FilePath?: string;
    FileContent?: number[];
    FileSize?: number;
    IsInline?: boolean;
    ContentId?: string;
    InsertDate?: string;
}

export abstract class EmailAttachmentsRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'FileName';
    static readonly localTextPrefix = 'Email.EmailAttachments';
    static readonly deletePermission = 'Email:EmailQueue:Delete';
    static readonly insertPermission = 'Email:EmailQueue:Insert';
    static readonly readPermission = 'Email:EmailQueue:Read';
    static readonly updatePermission = 'Email:EmailQueue:Update';

    static readonly Fields = fieldsProxy<EmailAttachmentsRow>();
}