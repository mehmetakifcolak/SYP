import { ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { WarehouseStockRow } from "./WarehouseStockRow";

export namespace WarehouseStockService {
    export const baseUrl = 'Warehouse/WarehouseStock';

    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<WarehouseStockRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<WarehouseStockRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<WarehouseStockRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<WarehouseStockRow>>;

    export const Methods = {
        Retrieve: "Warehouse/WarehouseStock/Retrieve",
        List: "Warehouse/WarehouseStock/List"
    } as const;

    [
        'Retrieve',
        'List'
    ].forEach(x => {
        (<any>WarehouseStockService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}