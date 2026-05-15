import { DeleteRequest, DeleteResponse, ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, SaveRequest, SaveResponse, ServiceOptions, serviceRequest, ServiceResponse } from "@serenity-is/corelib";
import { CancelEmailRequest } from "./CancelEmailRequest";
import { EmailQueueRow } from "./EmailQueueRow";
import { ResendEmailRequest } from "./ResendEmailRequest";

export namespace EmailQueueService {
    export const baseUrl = 'Email/EmailQueue';

    export declare function Create(request: SaveRequest<EmailQueueRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<EmailQueueRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<EmailQueueRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<EmailQueueRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<EmailQueueRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<EmailQueueRow>>;
    export declare function CancelEmail(request: CancelEmailRequest, onSuccess?: (response: ServiceResponse) => void, opt?: ServiceOptions<any>): PromiseLike<ServiceResponse>;
    export declare function ResendEmail(request: ResendEmailRequest, onSuccess?: (response: ServiceResponse) => void, opt?: ServiceOptions<any>): PromiseLike<ServiceResponse>;

    export const Methods = {
        Create: "Email/EmailQueue/Create",
        Update: "Email/EmailQueue/Update",
        Delete: "Email/EmailQueue/Delete",
        Retrieve: "Email/EmailQueue/Retrieve",
        List: "Email/EmailQueue/List",
        CancelEmail: "Email/EmailQueue/CancelEmail",
        ResendEmail: "Email/EmailQueue/ResendEmail"
    } as const;

    [
        'Create',
        'Update',
        'Delete',
        'Retrieve',
        'List',
        'CancelEmail',
        'ResendEmail'
    ].forEach(x => {
        (<any>EmailQueueService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}