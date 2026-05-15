import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { EmailAttachmentsRow } from "./EmailAttachmentsRow";

export interface EmailAttachmentsColumns {
    Id: Column<EmailAttachmentsRow>;
    EmailQueueId: Column<EmailAttachmentsRow>;
    FileName: Column<EmailAttachmentsRow>;
    ContentType: Column<EmailAttachmentsRow>;
    FileSize: Column<EmailAttachmentsRow>;
    IsInline: Column<EmailAttachmentsRow>;
    InsertDate: Column<EmailAttachmentsRow>;
}

export class EmailAttachmentsColumns extends ColumnsBase<EmailAttachmentsRow> {
    static readonly columnsKey = 'Email.EmailAttachments';
    static readonly Fields = fieldsProxy<EmailAttachmentsColumns>();
}