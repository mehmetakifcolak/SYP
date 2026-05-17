import { DeleteRequest, DeleteResponse, ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, SaveRequest, SaveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { OrderStatusHistRow } from "./OrderStatusHistRow";

export namespace OrderStatusHistService {
    export const baseUrl = 'Order/OrderStatusHist';

    export declare function Create(request: SaveRequest<OrderStatusHistRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<OrderStatusHistRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<OrderStatusHistRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<OrderStatusHistRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<OrderStatusHistRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<OrderStatusHistRow>>;

    export const Methods = {
        Create: "Order/OrderStatusHist/Create",
        Update: "Order/OrderStatusHist/Update",
        Delete: "Order/OrderStatusHist/Delete",
        Retrieve: "Order/OrderStatusHist/Retrieve",
        List: "Order/OrderStatusHist/List"
    } as const;

    [
        'Create',
        'Update',
        'Delete',
        'Retrieve',
        'List'
    ].forEach(x => {
        (<any>OrderStatusHistService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}