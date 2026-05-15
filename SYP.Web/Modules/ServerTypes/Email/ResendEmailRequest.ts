import { ServiceRequest } from "@serenity-is/corelib";

export interface ResendEmailRequest extends ServiceRequest {
    EmailId?: number;
}