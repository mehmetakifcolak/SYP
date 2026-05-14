import { SaveRequest, SaveResponse, ServiceOptions, DeleteRequest, DeleteResponse, RetrieveRequest, RetrieveResponse, ListRequest, ListResponse, serviceRequest } from '@serenity-is/corelib';
import { CustomersRow } from './CustomersRow';

export namespace CustomersService {
    export const baseUrl = 'Customer/Customers';

    export declare function Create(request: SaveRequest<CustomersRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<CustomersRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<CustomersRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<CustomersRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<CustomersRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<CustomersRow>>;

    export const Methods = {
        Create: "Customer/Customers/Create",
        Update: "Customer/Customers/Update",
        Delete: "Customer/Customers/Delete",
        Retrieve: "Customer/Customers/Retrieve",
        List: "Customer/Customers/List"
    } as const;

    [
        'Create', 
        'Update', 
        'Delete', 
        'Retrieve', 
        'List'
    ].forEach(x => {
        (<any>CustomersService)[x] = function (r, s, o) { 
            return serviceRequest(baseUrl + '/' + x, r, s, o); 
        };
    });
}