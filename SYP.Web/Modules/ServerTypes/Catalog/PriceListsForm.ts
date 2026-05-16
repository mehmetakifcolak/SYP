import { BooleanEditor, DateEditor, initFormType, IntegerEditor, LookupEditor, PrefixedContext, StringEditor, TextAreaEditor } from "@serenity-is/corelib";
import { PriceListItemsEditor } from "../../Catalog/PriceListItems/PriceListItemsEditor";

export interface PriceListsForm {
    Code: StringEditor;
    Name: StringEditor;
    CurrencyId: LookupEditor;
    ValidFrom: DateEditor;
    ValidTo: DateEditor;
    IsActive: IntegerEditor;
    IsDefault: BooleanEditor;
    Description: TextAreaEditor;
    ItemList: PriceListItemsEditor;
}

export class PriceListsForm extends PrefixedContext {
    static readonly formKey = 'Catalog.PriceLists';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!PriceListsForm.init) {
            PriceListsForm.init = true;

            var w0 = StringEditor;
            var w1 = LookupEditor;
            var w2 = DateEditor;
            var w3 = IntegerEditor;
            var w4 = BooleanEditor;
            var w5 = TextAreaEditor;
            var w6 = PriceListItemsEditor;

            initFormType(PriceListsForm, [
                'Code', w0,
                'Name', w0,
                'CurrencyId', w1,
                'ValidFrom', w2,
                'ValidTo', w2,
                'IsActive', w3,
                'IsDefault', w4,
                'Description', w5,
                'ItemList', w6
            ]);
        }
    }
}