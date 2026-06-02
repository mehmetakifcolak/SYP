import { ServiceResponse } from "@serenity-is/corelib";

export interface SendUserCredentialsResponse extends ServiceResponse {
    Email?: string;
}