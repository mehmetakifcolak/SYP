import { DeleteRequest, DeleteResponse, ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, SaveRequest, SaveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { CreateUserRequest } from "./CreateUserRequest";
import { CreateUserResponse } from "./CreateUserResponse";
import { CustomersRow } from "./CustomersRow";

export namespace CustomersService {
    export const baseUrl = 'Customer/Customers';

    export declare function Create(request: SaveRequest<CustomersRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<CustomersRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<CustomersRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<CustomersRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<CustomersRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<CustomersRow>>;
    export declare function CreateUser(request: CreateUserRequest, onSuccess?: (response: CreateUserResponse) => void, opt?: ServiceOptions<any>): PromiseLike<CreateUserResponse>;

    export const Methods = {
        Create: "Customer/Customers/Create",
        Update: "Customer/Customers/Update",
        Delete: "Customer/Customers/Delete",
        Retrieve: "Customer/Customers/Retrieve",
        List: "Customer/Customers/List",
        CreateUser: "Customer/Customers/CreateUser"
    } as const;

    [
        'Create',
        'Update',
        'Delete',
        'Retrieve',
        'List',
        'CreateUser'
    ].forEach(x => {
        (<any>CustomersService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}