import { registerEnum } from "@serenity-is/corelib";

export enum EmailQueueStatus {
    Pending = 0,
    Scheduled = 1,
    Processing = 2,
    Sent = 3,
    Failed = 4,
    Cancelled = 5,
    Retrying = 6
}
registerEnum(EmailQueueStatus, '_Ext.EmailQueueStatus', 'Email.EmailQueueStatus');