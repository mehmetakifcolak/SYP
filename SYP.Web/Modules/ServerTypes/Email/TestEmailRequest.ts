import { ServiceRequest } from "@serenity-is/corelib";

export interface TestEmailRequest extends ServiceRequest {
    SmtpSettingsId?: number;
    ToEmail?: string;
}