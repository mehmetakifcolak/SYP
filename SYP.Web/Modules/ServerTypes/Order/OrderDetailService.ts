import { DeleteRequest, DeleteResponse, ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, SaveRequest, SaveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { OrderDetailRow } from "./OrderDetailRow";

export namespace OrderDetailService {
    export const baseUrl = 'Order/OrderDetail';

    export declare function Create(request: SaveRequest<OrderDetailRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<OrderDetailRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<OrderDetailRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<OrderDetailRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<OrderDetailRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<OrderDetailRow>>;

    export const Methods = {
        Create: "Order/OrderDetail/Create",
        Update: "Order/OrderDetail/Update",
        Delete: "Order/OrderDetail/Delete",
        Retrieve: "Order/OrderDetail/Retrieve",
        List: "Order/OrderDetail/List"
    } as const;

    [
        'Create',
        'Update',
        'Delete',
        'Retrieve',
        'List'
    ].forEach(x => {
        (<any>OrderDetailService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}