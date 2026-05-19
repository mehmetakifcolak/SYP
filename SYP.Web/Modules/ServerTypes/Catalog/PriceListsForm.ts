import { BooleanEditor, DateEditor, EnumEditor, initFormType, LookupEditor, PrefixedContext, StringEditor, TextAreaEditor } from "@serenity-is/corelib";
import { PriceListItemsEditor } from "../../Catalog/PriceListItems/PriceListItemsEditor";
import { PriceListType } from "./PriceListType";

export interface PriceListsForm {
    Code: StringEditor;
    CurrencyId: LookupEditor;
    Name: StringEditor;
    Type: EnumEditor;
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
            var w2 = EnumEditor;
            var w3 = TextAreaEditor;
            var w4 = DateEditor;
            var w5 = BooleanEditor;
            var w6 = PriceListItemsEditor;

            initFormType(PriceListsForm, [
                'Code', w0,
                'CurrencyId', w1,
                'Name', w0,
                'Type', w2,
                'Description', w3,
                'ValidFrom', w4,
                'ValidTo', w4,
                'IsActive', w5,
                'IsDefault', w5,
                'ItemList', w6
            ]);
        }
    }
}

[PriceListType]; // referenced types