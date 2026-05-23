import { DeleteRequest, DeleteResponse, ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, SaveRequest, SaveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { BulkImportProductsRequest } from "./BulkImportProductsRequest";
import { BulkImportProductsResponse } from "./BulkImportProductsResponse";
import { ProductsRow } from "./ProductsRow";

export namespace ProductsService {
    export const baseUrl = 'Catalog/Products';

    export declare function Create(request: SaveRequest<ProductsRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<ProductsRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<ProductsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<ProductsRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<ProductsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<ProductsRow>>;
    export declare function BulkImportProducts(request: BulkImportProductsRequest, onSuccess?: (response: BulkImportProductsResponse) => void, opt?: ServiceOptions<any>): PromiseLike<BulkImportProductsResponse>;

    export const Methods = {
        Create: "Catalog/Products/Create",
        Update: "Catalog/Products/Update",
        Delete: "Catalog/Products/Delete",
        Retrieve: "Catalog/Products/Retrieve",
        List: "Catalog/Products/List",
        BulkImportProducts: "Catalog/Products/BulkImportProducts"
    } as const;

    [
        'Create',
        'Update',
        'Delete',
        'Retrieve',
        'List',
        'BulkImportProducts'
    ].forEach(x => {
        (<any>ProductsService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}