import { DateTimeEditor, EnumEditor, initFormType, LookupEditor, MultipleImageUploadEditor, PrefixedContext, StringEditor } from "@serenity-is/corelib";
import { StockExitDetailsEditor } from "../../Warehouse/StockExitDetails/StockExitDetailsEditor";
import { StockExitStatus } from "./StockExitStatus";

export interface StockExitsForm {
    ExitNo: StringEditor;
    WarehouseId: LookupEditor;
    ExitDate: DateTimeEditor;
    Status: EnumEditor;
    Description: StringEditor;
    DetailList: StockExitDetailsEditor;
    Attachments: MultipleImageUploadEditor;
}

export class StockExitsForm extends PrefixedContext {
    static readonly formKey = 'Warehouse.StockExits';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!StockExitsForm.init) {
            StockExitsForm.init = true;

            var w0 = StringEditor;
            var w1 = LookupEditor;
            var w2 = DateTimeEditor;
            var w3 = EnumEditor;
            var w4 = StockExitDetailsEditor;
            var w5 = MultipleImageUploadEditor;

            initFormType(StockExitsForm, [
                'ExitNo', w0,
                'WarehouseId', w1,
                'ExitDate', w2,
                'Status', w3,
                'Description', w0,
                'DetailList', w4,
                'Attachments', w5
            ]);
        }
    }
}

[StockExitStatus]; // referenced types