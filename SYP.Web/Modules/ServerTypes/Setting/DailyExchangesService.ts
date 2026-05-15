import { DeleteRequest, DeleteResponse, ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, SaveRequest, SaveResponse, ServiceOptions, ServiceRequest, serviceRequest, ServiceResponse } from "@serenity-is/corelib";
import { DailyExchangesRow } from "./DailyExchangesRow";
import { ImportDateRangeRequest } from "./ImportDateRangeRequest";

export namespace DailyExchangesService {
    export const baseUrl = 'Setting/DailyExchanges';

    export declare function Create(request: SaveRequest<DailyExchangesRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<DailyExchangesRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<DailyExchangesRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<DailyExchangesRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<DailyExchangesRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<DailyExchangesRow>>;
    export declare function ImportDateRange(request: ImportDateRangeRequest, onSuccess?: (response: ServiceResponse) => void, opt?: ServiceOptions<any>): PromiseLike<ServiceResponse>;
    export declare function FetchTodayRates(request: ServiceRequest, onSuccess?: (response: ServiceResponse) => void, opt?: ServiceOptions<any>): PromiseLike<ServiceResponse>;

    export const Methods = {
        Create: "Setting/DailyExchanges/Create",
        Update: "Setting/DailyExchanges/Update",
        Delete: "Setting/DailyExchanges/Delete",
        Retrieve: "Setting/DailyExchanges/Retrieve",
        List: "Setting/DailyExchanges/List",
        ImportDateRange: "Setting/DailyExchanges/ImportDateRange",
        FetchTodayRates: "Setting/DailyExchanges/FetchTodayRates"
    } as const;

    [
        'Create',
        'Update',
        'Delete',
        'Retrieve',
        'List',
        'ImportDateRange',
        'FetchTodayRates'
    ].forEach(x => {
        (<any>DailyExchangesService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}