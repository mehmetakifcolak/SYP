import { ServiceRequest } from "@serenity-is/corelib";

export interface ImportDateRangeRequest extends ServiceRequest {
    StartDate?: string;
    EndDate?: string;
}