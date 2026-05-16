import { DeleteRequest, DeleteResponse, ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, SaveRequest, SaveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { PriceListsRow } from "./PriceListsRow";

export namespace PriceListsService {
    export const baseUrl = 'Catalog/PriceLists';

    export declare function Create(request: SaveRequest<PriceListsRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<PriceListsRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<PriceListsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<PriceListsRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<PriceListsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<PriceListsRow>>;

    export const Methods = {
        Create: "Catalog/PriceLists/Create",
        Update: "Catalog/PriceLists/Update",
        Delete: "Catalog/PriceLists/Delete",
        Retrieve: "Catalog/PriceLists/Retrieve",
        List: "Catalog/PriceLists/List"
    } as const;

    [
        'Create',
        'Update',
        'Delete',
        'Retrieve',
        'List'
    ].forEach(x => {
        (<any>PriceListsService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}