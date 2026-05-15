import { ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { EmailAttachmentsRow } from "./EmailAttachmentsRow";

export namespace EmailAttachmentsService {
    export const baseUrl = 'Email/EmailAttachments';

    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<EmailAttachmentsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<EmailAttachmentsRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<EmailAttachmentsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<EmailAttachmentsRow>>;

    export const Methods = {
        Retrieve: "Email/EmailAttachments/Retrieve",
        List: "Email/EmailAttachments/List"
    } as const;

    [
        'Retrieve',
        'List'
    ].forEach(x => {
        (<any>EmailAttachmentsService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}