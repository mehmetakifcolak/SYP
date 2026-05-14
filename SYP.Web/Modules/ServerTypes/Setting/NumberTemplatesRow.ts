import { fieldsProxy } from "@serenity-is/corelib";
import { NumberTemplateType } from "../_Ext/NumberTemplateType";

export interface NumberTemplatesRow {
    Id?: number;
    Prefix?: string;
    DateFormat?: string;
    Suffix?: string;
    Length?: number;
    LengthText?: number;
    Active?: boolean;
    Type?: NumberTemplateType;
    DepartmentId?: number;
    InsertUserId?: number;
    InsertDate?: string;
    UpdateUserId?: number;
    UpdateDate?: string;
    TenantId?: number;
}

export abstract class NumberTemplatesRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'Prefix';
    static readonly localTextPrefix = 'Setting.NumberTemplates';
    static readonly deletePermission = 'Setting:NumberTemplates:Delete';
    static readonly insertPermission = 'Setting:NumberTemplates:Insert';
    static readonly readPermission = 'Setting:NumberTemplates:Read';
    static readonly updatePermission = 'Setting:NumberTemplates:Update';

    static readonly Fields = fieldsProxy<NumberTemplatesRow>();
}