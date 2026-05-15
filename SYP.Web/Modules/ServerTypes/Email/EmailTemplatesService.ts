import { DeleteRequest, DeleteResponse, ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, SaveRequest, SaveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { EmailTemplatesRow } from "./EmailTemplatesRow";

export namespace EmailTemplatesService {
    export const baseUrl = 'Email/EmailTemplates';

    export declare function Create(request: SaveRequest<EmailTemplatesRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<EmailTemplatesRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<EmailTemplatesRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<EmailTemplatesRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<EmailTemplatesRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<EmailTemplatesRow>>;

    export const Methods = {
        Create: "Email/EmailTemplates/Create",
        Update: "Email/EmailTemplates/Update",
        Delete: "Email/EmailTemplates/Delete",
        Retrieve: "Email/EmailTemplates/Retrieve",
        List: "Email/EmailTemplates/List"
    } as const;

    [
        'Create',
        'Update',
        'Delete',
        'Retrieve',
        'List'
    ].forEach(x => {
        (<any>EmailTemplatesService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}