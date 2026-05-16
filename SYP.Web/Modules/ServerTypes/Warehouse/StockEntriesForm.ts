import { DateTimeEditor, EnumEditor, initFormType, LookupEditor, MultipleImageUploadEditor, PrefixedContext, StringEditor } from "@serenity-is/corelib";
import { StockEntryDetailsEditor } from "../../Warehouse/StockEntryDetails/StockEntryDetailsEditor";
import { StockEntryStatus } from "../_Ext/StockEntryStatus";

export interface StockEntriesForm {
    EntryNo: StringEditor;
    WarehouseId: LookupEditor;
    EntryDate: DateTimeEditor;
    Status: EnumEditor;
    Description: StringEditor;
    DetailList: StockEntryDetailsEditor;
    Attachments: MultipleImageUploadEditor;
}

export class StockEntriesForm extends PrefixedContext {
    static readonly formKey = 'Warehouse.StockEntries';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!StockEntriesForm.init) {
            StockEntriesForm.init = true;

            var w0 = StringEditor;
            var w1 = LookupEditor;
            var w2 = DateTimeEditor;
            var w3 = EnumEditor;
            var w4 = StockEntryDetailsEditor;
            var w5 = MultipleImageUploadEditor;

            initFormType(StockEntriesForm, [
                'EntryNo', w0,
                'WarehouseId', w1,
                'EntryDate', w2,
                'Status', w3,
                'Description', w0,
                'DetailList', w4,
                'Attachments', w5
            ]);
        }
    }
}

[StockEntryStatus]; // referenced types