import { fieldsProxy, getLookup, getLookupAsync } from "@serenity-is/corelib";

export interface EmailTemplatesRow {
    Id?: number;
    TemplateKey?: string;
    Name?: string;
    Subject?: string;
    Body?: string;
    BodyText?: string;
    LanguageId?: string;
    Category?: string;
    Description?: string;
    IsActive?: boolean;
    InsertDate?: string;
    InsertUserId?: number;
    UpdateDate?: string;
    UpdateUserId?: number;
}

export abstract class EmailTemplatesRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'Name';
    static readonly localTextPrefix = 'Email.EmailTemplates';
    static readonly lookupKey = 'Email.EmailTemplates';

    /** @deprecated use getLookupAsync instead */
    static getLookup() { return getLookup<EmailTemplatesRow>('Email.EmailTemplates') }
    static async getLookupAsync() { return getLookupAsync<EmailTemplatesRow>('Email.EmailTemplates') }

    static readonly deletePermission = 'Email:EmailTemplates:Delete';
    static readonly insertPermission = 'Email:EmailTemplates:Insert';
    static readonly readPermission = 'Email:EmailTemplates:Read';
    static readonly updatePermission = 'Email:EmailTemplates:Update';

    static readonly Fields = fieldsProxy<EmailTemplatesRow>();
}