import { ServiceRequest } from "@serenity-is/corelib";

export interface CreateUserRequest extends ServiceRequest {
    CustomerId?: number;
    Password?: string;
    PasswordConfirm?: string;
}