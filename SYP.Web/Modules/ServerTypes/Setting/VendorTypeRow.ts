import { fieldsProxy, getLookup, getLookupAsync } from "@serenity-is/corelib";

export interface VendorTypeRow {
    Id?: number;
    Title?: string;
    DiscountType?: string;
    DiscountValue?: number;
    Description?: string;
    IsActive?: boolean;
}

export abstract class VendorTypeRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'Title';
    static readonly localTextPrefix = 'Setting.VendorType';
    static readonly lookupKey = 'Setting.VendorType';

    /** @deprecated use getLookupAsync instead */
    static getLookup() { return getLookup<VendorTypeRow>('Setting.VendorType') }
    static async getLookupAsync() { return getLookupAsync<VendorTypeRow>('Setting.VendorType') }

    static readonly deletePermission = 'Setting:VendorType:Delete';
    static readonly insertPermission = 'Setting:VendorType:Insert';
    static readonly readPermission = 'Setting:VendorType:Read';
    static readonly updatePermission = 'Setting:VendorType:Update';

    static readonly Fields = fieldsProxy<VendorTypeRow>();
}