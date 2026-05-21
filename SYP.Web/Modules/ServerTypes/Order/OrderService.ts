import { DeleteRequest, DeleteResponse, ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, SaveRequest, SaveResponse, ServiceOptions, ServiceRequest, serviceRequest, ServiceResponse } from "@serenity-is/corelib";
import { GetAllowedTransitionsRequest } from "./GetAllowedTransitionsRequest";
import { GetAllowedTransitionsResponse } from "./GetAllowedTransitionsResponse";
import { GetBayiiCustomerResponse } from "./GetBayiiCustomerResponse";
import { OrderRow } from "./OrderRow";
import { UploadDekontRequest } from "./UploadDekontRequest";

export namespace OrderService {
    export const baseUrl = 'Order/Order';

    export declare function Create(request: SaveRequest<OrderRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<OrderRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<OrderRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<OrderRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<OrderRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<OrderRow>>;
    export declare function GetCurrentBayiiCustomerId(request: ServiceRequest, onSuccess?: (response: GetBayiiCustomerResponse) => void, opt?: ServiceOptions<any>): PromiseLike<GetBayiiCustomerResponse>;
    export declare function GetAllowedTransitions(request: GetAllowedTransitionsRequest, onSuccess?: (response: GetAllowedTransitionsResponse) => void, opt?: ServiceOptions<any>): PromiseLike<GetAllowedTransitionsResponse>;
    export declare function UploadDekont(request: UploadDekontRequest, onSuccess?: (response: ServiceResponse) => void, opt?: ServiceOptions<any>): PromiseLike<ServiceResponse>;

    export const Methods = {
        Create: "Order/Order/Create",
        Update: "Order/Order/Update",
        Delete: "Order/Order/Delete",
        Retrieve: "Order/Order/Retrieve",
        List: "Order/Order/List",
        GetCurrentBayiiCustomerId: "Order/Order/GetCurrentBayiiCustomerId",
        GetAllowedTransitions: "Order/Order/GetAllowedTransitions",
        UploadDekont: "Order/Order/UploadDekont"
    } as const;

    [
        'Create',
        'Update',
        'Delete',
        'Retrieve',
        'List',
        'GetCurrentBayiiCustomerId',
        'GetAllowedTransitions',
        'UploadDekont'
    ].forEach(x => {
        (<any>OrderService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}