import { ServiceResponse } from "@serenity-is/corelib";

export interface GetImpersonateTokenResponse extends ServiceResponse {
    Token?: string;
}