import { ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { StockMovementsRow } from "./StockMovementsRow";

export namespace StockMovementsService {
    export const baseUrl = 'Warehouse/StockMovements';

    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<StockMovementsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<StockMovementsRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<StockMovementsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<StockMovementsRow>>;

    export const Methods = {
        Retrieve: "Warehouse/StockMovements/Retrieve",
        List: "Warehouse/StockMovements/List"
    } as const;

    [
        'Retrieve',
        'List'
    ].forEach(x => {
        (<any>StockMovementsService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}
