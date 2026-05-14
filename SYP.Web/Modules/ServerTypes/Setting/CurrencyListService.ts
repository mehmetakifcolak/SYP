import { DeleteRequest, DeleteResponse, ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, SaveRequest, SaveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { CurrencyListRow } from "./CurrencyListRow";

export namespace CurrencyListService {
    export const baseUrl = 'Setting/CurrencyList';

    export declare function Create(request: SaveRequest<CurrencyListRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<CurrencyListRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<CurrencyListRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<CurrencyListRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<CurrencyListRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<CurrencyListRow>>;

    export const Methods = {
        Create: "Setting/CurrencyList/Create",
        Update: "Setting/CurrencyList/Update",
        Delete: "Setting/CurrencyList/Delete",
        Retrieve: "Setting/CurrencyList/Retrieve",
        List: "Setting/CurrencyList/List"
    } as const;

    [
        'Create',
        'Update',
        'Delete',
        'Retrieve',
        'List'
    ].forEach(x => {
        (<any>CurrencyListService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}