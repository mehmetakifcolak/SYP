import { DeleteRequest, DeleteResponse, ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, SaveRequest, SaveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { ProductCategoryRow } from "./ProductCategoryRow";

export namespace ProductCategoryService {
    export const baseUrl = 'Catalog/ProductCategory';

    export declare function Create(request: SaveRequest<ProductCategoryRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<ProductCategoryRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<ProductCategoryRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<ProductCategoryRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<ProductCategoryRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<ProductCategoryRow>>;

    export const Methods = {
        Create: "Catalog/ProductCategory/Create",
        Update: "Catalog/ProductCategory/Update",
        Delete: "Catalog/ProductCategory/Delete",
        Retrieve: "Catalog/ProductCategory/Retrieve",
        List: "Catalog/ProductCategory/List"
    } as const;

    [
        'Create',
        'Update',
        'Delete',
        'Retrieve',
        'List'
    ].forEach(x => {
        (<any>ProductCategoryService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}