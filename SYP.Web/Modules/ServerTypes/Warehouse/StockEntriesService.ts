import { DeleteRequest, DeleteResponse, ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, SaveRequest, SaveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { StockEntriesRow } from "./StockEntriesRow";

export namespace StockEntriesService {
    export const baseUrl = 'Warehouse/StockEntries';

    export declare function Create(request: SaveRequest<StockEntriesRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<StockEntriesRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<StockEntriesRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<StockEntriesRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<StockEntriesRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<StockEntriesRow>>;

    export const Methods = {
        Create: "Warehouse/StockEntries/Create",
        Update: "Warehouse/StockEntries/Update",
        Delete: "Warehouse/StockEntries/Delete",
        Retrieve: "Warehouse/StockEntries/Retrieve",
        List: "Warehouse/StockEntries/List"
    } as const;

    [
        'Create',
        'Update',
        'Delete',
        'Retrieve',
        'List'
    ].forEach(x => {
        (<any>StockEntriesService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}