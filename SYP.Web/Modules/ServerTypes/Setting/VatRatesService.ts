import { DeleteRequest, DeleteResponse, ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, SaveRequest, SaveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { VatRatesRow } from "./VatRatesRow";

export namespace VatRatesService {
    export const baseUrl = 'Setting/VatRates';

    export declare function Create(request: SaveRequest<VatRatesRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<VatRatesRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<VatRatesRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<VatRatesRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<VatRatesRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<VatRatesRow>>;

    export const Methods = {
        Create: "Setting/VatRates/Create",
        Update: "Setting/VatRates/Update",
        Delete: "Setting/VatRates/Delete",
        Retrieve: "Setting/VatRates/Retrieve",
        List: "Setting/VatRates/List"
    } as const;

    [
        'Create',
        'Update',
        'Delete',
        'Retrieve',
        'List'
    ].forEach(x => {
        (<any>VatRatesService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}