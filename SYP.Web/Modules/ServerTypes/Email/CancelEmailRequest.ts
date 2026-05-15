import { ServiceRequest } from "@serenity-is/corelib";

export interface CancelEmailRequest extends ServiceRequest {
    EmailId?: number;
}