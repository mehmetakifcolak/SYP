import { ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { AuditLogRow } from "./AuditLogRow";

export namespace AuditLogService {
    export const baseUrl = 'Administration/AuditLog';

    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<AuditLogRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<AuditLogRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<AuditLogRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<AuditLogRow>>;

    export const Methods = {
        Retrieve: "Administration/AuditLog/Retrieve",
        List: "Administration/AuditLog/List"
    } as const;

    [
        'Retrieve',
        'List'
    ].forEach(x => {
        (<any>AuditLogService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}