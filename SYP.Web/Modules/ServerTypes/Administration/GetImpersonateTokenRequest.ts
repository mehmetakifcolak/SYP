import { ServiceRequest } from "@serenity-is/corelib";

export interface GetImpersonateTokenRequest extends ServiceRequest {
    UserId?: number;
}