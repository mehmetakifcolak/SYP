import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { EmailTemplatesRow } from "./EmailTemplatesRow";

export interface EmailTemplatesColumns {
    Id: Column<EmailTemplatesRow>;
    TemplateKey: Column<EmailTemplatesRow>;
    Name: Column<EmailTemplatesRow>;
    Subject: Column<EmailTemplatesRow>;
    Category: Column<EmailTemplatesRow>;
    LanguageId: Column<EmailTemplatesRow>;
    IsActive: Column<EmailTemplatesRow>;
}

export class EmailTemplatesColumns extends ColumnsBase<EmailTemplatesRow> {
    static readonly columnsKey = 'Email.EmailTemplates';
    static readonly Fields = fieldsProxy<EmailTemplatesColumns>();
}