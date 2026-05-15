import { DeleteRequest, DeleteResponse, ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, SaveRequest, SaveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { VendorTypeRow } from "./VendorTypeRow";

export namespace VendorTypeService {
    export const baseUrl = 'Setting/VendorType';

    export declare function Create(request: SaveRequest<VendorTypeRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<VendorTypeRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<VendorTypeRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<VendorTypeRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<VendorTypeRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<VendorTypeRow>>;

    export const Methods = {
        Create: "Setting/VendorType/Create",
        Update: "Setting/VendorType/Update",
        Delete: "Setting/VendorType/Delete",
        Retrieve: "Setting/VendorType/Retrieve",
        List: "Setting/VendorType/List"
    } as const;

    [
        'Create',
        'Update',
        'Delete',
        'Retrieve',
        'List'
    ].forEach(x => {
        (<any>VendorTypeService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}