import { DeleteRequest, DeleteResponse, ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, SaveRequest, SaveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { CountryRow } from "./CountryRow";

export namespace CountryService {
    export const baseUrl = 'Setting/Country';

    export declare function Create(request: SaveRequest<CountryRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<CountryRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<CountryRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<CountryRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<CountryRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<CountryRow>>;

    export const Methods = {
        Create: "Setting/Country/Create",
        Update: "Setting/Country/Update",
        Delete: "Setting/Country/Delete",
        Retrieve: "Setting/Country/Retrieve",
        List: "Setting/Country/List"
    } as const;

    [
        'Create',
        'Update',
        'Delete',
        'Retrieve',
        'List'
    ].forEach(x => {
        (<any>CountryService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}