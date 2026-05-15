import { DeleteRequest, DeleteResponse, ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, SaveRequest, SaveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { WarehousesRow } from "./WarehousesRow";

export namespace WarehousesService {
    export const baseUrl = 'Warehouse/Warehouses';

    export declare function Create(request: SaveRequest<WarehousesRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<WarehousesRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<WarehousesRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<WarehousesRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<WarehousesRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<WarehousesRow>>;

    export const Methods = {
        Create: "Warehouse/Warehouses/Create",
        Update: "Warehouse/Warehouses/Update",
        Delete: "Warehouse/Warehouses/Delete",
        Retrieve: "Warehouse/Warehouses/Retrieve",
        List: "Warehouse/Warehouses/List"
    } as const;

    [
        'Create',
        'Update',
        'Delete',
        'Retrieve',
        'List'
    ].forEach(x => {
        (<any>WarehousesService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}