import { DeleteRequest, DeleteResponse, ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, SaveRequest, SaveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { TieredDiscountSettingsRow } from "./TieredDiscountSettingsRow";

export namespace TieredDiscountSettingsService {
    export const baseUrl = 'Order/TieredDiscountSettings';

    export declare function Create(request: SaveRequest<TieredDiscountSettingsRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<TieredDiscountSettingsRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<TieredDiscountSettingsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<TieredDiscountSettingsRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<TieredDiscountSettingsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<TieredDiscountSettingsRow>>;

    export const Methods = {
        Create: "Order/TieredDiscountSettings/Create",
        Update: "Order/TieredDiscountSettings/Update",
        Delete: "Order/TieredDiscountSettings/Delete",
        Retrieve: "Order/TieredDiscountSettings/Retrieve",
        List: "Order/TieredDiscountSettings/List"
    } as const;

    [
        'Create',
        'Update',
        'Delete',
        'Retrieve',
        'List'
    ].forEach(x => {
        (<any>TieredDiscountSettingsService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}