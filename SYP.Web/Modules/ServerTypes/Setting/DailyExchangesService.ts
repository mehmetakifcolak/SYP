import { DeleteRequest, DeleteResponse, ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, SaveRequest, SaveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { DailyExchangesRow } from "./DailyExchangesRow";

export namespace DailyExchangesService {
    export const baseUrl = 'Setting/DailyExchanges';

    export declare function Create(request: SaveRequest<DailyExchangesRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<DailyExchangesRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<DailyExchangesRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<DailyExchangesRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<DailyExchangesRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<DailyExchangesRow>>;

    export const Methods = {
        Create: "Setting/DailyExchanges/Create",
        Update: "Setting/DailyExchanges/Update",
        Delete: "Setting/DailyExchanges/Delete",
        Retrieve: "Setting/DailyExchanges/Retrieve",
        List: "Setting/DailyExchanges/List"
    } as const;

    [
        'Create',
        'Update',
        'Delete',
        'Retrieve',
        'List'
    ].forEach(x => {
        (<any>DailyExchangesService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}