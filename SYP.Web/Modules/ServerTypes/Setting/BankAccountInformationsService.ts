import { DeleteRequest, DeleteResponse, ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, SaveRequest, SaveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { BankAccountInformationsRow } from "./BankAccountInformationsRow";

export namespace BankAccountInformationsService {
    export const baseUrl = 'Setting/BankAccountInformations';

    export declare function Create(request: SaveRequest<BankAccountInformationsRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<BankAccountInformationsRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<BankAccountInformationsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<BankAccountInformationsRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<BankAccountInformationsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<BankAccountInformationsRow>>;

    export const Methods = {
        Create: "Setting/BankAccountInformations/Create",
        Update: "Setting/BankAccountInformations/Update",
        Delete: "Setting/BankAccountInformations/Delete",
        Retrieve: "Setting/BankAccountInformations/Retrieve",
        List: "Setting/BankAccountInformations/List"
    } as const;

    [
        'Create',
        'Update',
        'Delete',
        'Retrieve',
        'List'
    ].forEach(x => {
        (<any>BankAccountInformationsService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}