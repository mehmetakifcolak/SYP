import { ServiceResponse } from "@serenity-is/corelib";

export interface CreateUserResponse extends ServiceResponse {
    UserId?: number;
    Username?: string;
}