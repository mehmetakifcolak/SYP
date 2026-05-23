import { ServiceResponse } from "@serenity-is/corelib";
import { ProductImportError } from "./ProductImportError";

export interface BulkImportProductsResponse extends ServiceResponse {
    SuccessCount?: number;
    ErrorCount?: number;
    Errors?: ProductImportError[];
}