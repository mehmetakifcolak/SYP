import { ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { EmailLogsRow } from "./EmailLogsRow";

export namespace EmailLogsService {
    export const baseUrl = 'Email/EmailLogs';

    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<EmailLogsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<EmailLogsRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<EmailLogsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<EmailLogsRow>>;

    export const Methods = {
        Retrieve: "Email/EmailLogs/Retrieve",
        List: "Email/EmailLogs/List"
    } as const;

    [
        'Retrieve',
        'List'
    ].forEach(x => {
        (<any>EmailLogsService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}