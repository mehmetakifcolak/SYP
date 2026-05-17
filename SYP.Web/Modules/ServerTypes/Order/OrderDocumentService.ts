import { DeleteRequest, DeleteResponse, ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, SaveRequest, SaveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { OrderDocumentRow } from "./OrderDocumentRow";

export namespace OrderDocumentService {
    export const baseUrl = 'Order/OrderDocument';

    export declare function Create(request: SaveRequest<OrderDocumentRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<OrderDocumentRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<OrderDocumentRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<OrderDocumentRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<OrderDocumentRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<OrderDocumentRow>>;

    export const Methods = {
        Create: "Order/OrderDocument/Create",
        Update: "Order/OrderDocument/Update",
        Delete: "Order/OrderDocument/Delete",
        Retrieve: "Order/OrderDocument/Retrieve",
        List: "Order/OrderDocument/List"
    } as const;

    [
        'Create',
        'Update',
        'Delete',
        'Retrieve',
        'List'
    ].forEach(x => {
        (<any>OrderDocumentService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}