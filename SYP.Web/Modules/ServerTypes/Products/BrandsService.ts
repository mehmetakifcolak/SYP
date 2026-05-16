import { DeleteRequest, DeleteResponse, ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, SaveRequest, SaveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { BrandsRow } from "./BrandsRow";

export namespace BrandsService {
    export const baseUrl = 'Products/Brands';

    export declare function Create(request: SaveRequest<BrandsRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<BrandsRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<BrandsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<BrandsRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<BrandsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<BrandsRow>>;

    export const Methods = {
        Create: "Products/Brands/Create",
        Update: "Products/Brands/Update",
        Delete: "Products/Brands/Delete",
        Retrieve: "Products/Brands/Retrieve",
        List: "Products/Brands/List"
    } as const;

    [
        'Create',
        'Update',
        'Delete',
        'Retrieve',
        'List'
    ].forEach(x => {
        (<any>BrandsService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}