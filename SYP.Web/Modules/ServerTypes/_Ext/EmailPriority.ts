import { registerEnum } from "@serenity-is/corelib";

export enum EmailPriority {
    High = 1,
    Normal = 2,
    Low = 3
}
registerEnum(EmailPriority, '_Ext.EmailPriority', 'Email.EmailPriority');