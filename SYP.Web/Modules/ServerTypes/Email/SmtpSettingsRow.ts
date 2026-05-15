import { fieldsProxy, getLookup, getLookupAsync } from "@serenity-is/corelib";

export interface SmtpSettingsRow {
    Id?: number;
    Name?: string;
    Host?: string;
    Port?: number;
    UseSsl?: boolean;
    Username?: string;
    Password?: string;
    FromAddress?: string;
    FromName?: string;
    IsDefault?: boolean;
    IsActive?: boolean;
    MaxRetryCount?: number;
    RetryIntervalMinutes?: number;
    DailyLimit?: number;
    InsertDate?: string;
    InsertUserId?: number;
    UpdateDate?: string;
    UpdateUserId?: number;
}

export abstract class SmtpSettingsRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'Name';
    static readonly localTextPrefix = 'Email.SmtpSettings';
    static readonly lookupKey = 'Email.SmtpSettings';

    /** @deprecated use getLookupAsync instead */
    static getLookup() { return getLookup<SmtpSettingsRow>('Email.SmtpSettings') }
    static async getLookupAsync() { return getLookupAsync<SmtpSettingsRow>('Email.SmtpSettings') }

    static readonly deletePermission = 'Email:SmtpSettings:Delete';
    static readonly insertPermission = 'Email:SmtpSettings:Insert';
    static readonly readPermission = 'Email:SmtpSettings:Read';
    static readonly updatePermission = 'Email:SmtpSettings:Update';

    static readonly Fields = fieldsProxy<SmtpSettingsRow>();
}