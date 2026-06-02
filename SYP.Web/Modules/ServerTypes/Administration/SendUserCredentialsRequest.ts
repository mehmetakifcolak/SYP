import { ServiceRequest } from "@serenity-is/corelib";

export interface SendUserCredentialsRequest extends ServiceRequest {
    UserId?: number;
}