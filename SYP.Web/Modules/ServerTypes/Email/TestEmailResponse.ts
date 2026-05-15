import { ServiceResponse } from "@serenity-is/corelib";

export interface TestEmailResponse extends ServiceResponse {
    Success?: boolean;
    Message?: string;
}