import { DeleteRequest, DeleteResponse, ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, SaveRequest, SaveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { NumberTemplatesRow } from "./NumberTemplatesRow";

export namespace NumberTemplatesService {
    export const baseUrl = 'Setting/NumberTemplates';

    export declare function Create(request: SaveRequest<NumberTemplatesRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<NumberTemplatesRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<NumberTemplatesRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<NumberTemplatesRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<NumberTemplatesRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<NumberTemplatesRow>>;

    export const Methods = {
        Create: "Setting/NumberTemplates/Create",
        Update: "Setting/NumberTemplates/Update",
        Delete: "Setting/NumberTemplates/Delete",
        Retrieve: "Setting/NumberTemplates/Retrieve",
        List: "Setting/NumberTemplates/List"
    } as const;

    [
        'Create',
        'Update',
        'Delete',
        'Retrieve',
        'List'
    ].forEach(x => {
        (<any>NumberTemplatesService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}