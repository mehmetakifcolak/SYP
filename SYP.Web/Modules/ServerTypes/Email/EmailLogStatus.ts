import { registerEnum } from "@serenity-is/corelib";

export enum EmailLogStatus {
    Queued = 0,
    Processing = 1,
    Sent = 2,
    Failed = 3
}
registerEnum(EmailLogStatus, 'SYP.Email.EmailLogStatus', 'Email.EmailLogStatus');