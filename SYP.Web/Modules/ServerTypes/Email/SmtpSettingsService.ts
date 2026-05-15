import { DeleteRequest, DeleteResponse, ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, SaveRequest, SaveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { SmtpSettingsRow } from "./SmtpSettingsRow";
import { TestEmailRequest } from "./TestEmailRequest";
import { TestEmailResponse } from "./TestEmailResponse";

export namespace SmtpSettingsService {
    export const baseUrl = 'Email/SmtpSettings';

    export declare function Create(request: SaveRequest<SmtpSettingsRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<SmtpSettingsRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<SmtpSettingsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<SmtpSettingsRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<SmtpSettingsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<SmtpSettingsRow>>;
    export declare function TestEmail(request: TestEmailRequest, onSuccess?: (response: TestEmailResponse) => void, opt?: ServiceOptions<any>): PromiseLike<TestEmailResponse>;

    export const Methods = {
        Create: "Email/SmtpSettings/Create",
        Update: "Email/SmtpSettings/Update",
        Delete: "Email/SmtpSettings/Delete",
        Retrieve: "Email/SmtpSettings/Retrieve",
        List: "Email/SmtpSettings/List",
        TestEmail: "Email/SmtpSettings/TestEmail"
    } as const;

    [
        'Create',
        'Update',
        'Delete',
        'Retrieve',
        'List',
        'TestEmail'
    ].forEach(x => {
        (<any>SmtpSettingsService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}