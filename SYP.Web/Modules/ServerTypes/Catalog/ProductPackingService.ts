import { DeleteRequest, DeleteResponse, ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, SaveRequest, SaveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { ProductPackingRow } from "./ProductPackingRow";

export namespace ProductPackingService {
    export const baseUrl = 'Catalog/ProductPacking';

    export declare function Create(request: SaveRequest<ProductPackingRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<ProductPackingRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<ProductPackingRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<ProductPackingRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<ProductPackingRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<ProductPackingRow>>;

    export const Methods = {
        Create: "Catalog/ProductPacking/Create",
        Update: "Catalog/ProductPacking/Update",
        Delete: "Catalog/ProductPacking/Delete",
        Retrieve: "Catalog/ProductPacking/Retrieve",
        List: "Catalog/ProductPacking/List"
    } as const;

    [
        'Create',
        'Update',
        'Delete',
        'Retrieve',
        'List'
    ].forEach(x => {
        (<any>ProductPackingService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}