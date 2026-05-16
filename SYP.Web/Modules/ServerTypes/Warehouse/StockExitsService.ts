import { DeleteRequest, DeleteResponse, ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, SaveRequest, SaveResponse, ServiceOptions, ServiceRequest, serviceRequest } from "@serenity-is/corelib";
import { GetNextNumberResponse } from "../GetNextNumberResponse";
import { StockExitsRow } from "./StockExitsRow";

export namespace StockExitsService {
    export const baseUrl = 'Warehouse/StockExits';

    export declare function Create(request: SaveRequest<StockExitsRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<StockExitsRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<StockExitsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<StockExitsRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<StockExitsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<StockExitsRow>>;
    export declare function GetNextExitNo(request: ServiceRequest, onSuccess?: (response: GetNextNumberResponse) => void, opt?: ServiceOptions<any>): PromiseLike<GetNextNumberResponse>;

    export const Methods = {
        Create: "Warehouse/StockExits/Create",
        Update: "Warehouse/StockExits/Update",
        Delete: "Warehouse/StockExits/Delete",
        Retrieve: "Warehouse/StockExits/Retrieve",
        List: "Warehouse/StockExits/List",
        GetNextExitNo: "Warehouse/StockExits/GetNextExitNo"
    } as const;

    [
        'Create',
        'Update',
        'Delete',
        'Retrieve',
        'List',
        'GetNextExitNo'
    ].forEach(x => {
        (<any>StockExitsService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}