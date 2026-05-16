import { ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { PriceListItemsRow } from "./PriceListItemsRow";

export namespace PriceListItemsService {
    export const baseUrl = 'Catalog/PriceListItems';

    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<PriceListItemsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<PriceListItemsRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<PriceListItemsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<PriceListItemsRow>>;

    export const Methods = {
        Retrieve: "Catalog/PriceListItems/Retrieve",
        List: "Catalog/PriceListItems/List"
    } as const;

    [
        'Retrieve',
        'List'
    ].forEach(x => {
        (<any>PriceListItemsService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}