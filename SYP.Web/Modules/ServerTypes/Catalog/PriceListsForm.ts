import { BooleanEditor, DateEditor, initFormType, LookupEditor, PrefixedContext, StringEditor, TextAreaEditor } from "@serenity-is/corelib";
import { PriceListItemsEditor } from "../../Catalog/PriceListItems/PriceListItemsEditor";

export interface PriceListsForm {
    Code: StringEditor;
    CurrencyId: LookupEditor;
    Name: StringEditor;
    Description: TextAreaEditor;
    ValidFrom: DateEditor;
    ValidTo: DateEditor;
    IsActive: BooleanEditor;
    IsDefault: BooleanEditor;
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
            var w2 = TextAreaEditor;
            var w3 = DateEditor;
            var w4 = BooleanEditor;
            var w5 = PriceListItemsEditor;

            initFormType(PriceListsForm, [
                'Code', w0,
                'CurrencyId', w1,
                'Name', w0,
                'Description', w2,
                'ValidFrom', w3,
                'ValidTo', w3,
                'IsActive', w4,
                'IsDefault', w4,
                'ItemList', w5
            ]);
        }
    }
}