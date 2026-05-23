import { ServiceRequest } from "@serenity-is/corelib";
import { ProductImportItem } from "./ProductImportItem";

export interface BulkImportProductsRequest extends ServiceRequest {
    Products?: ProductImportItem[];
}