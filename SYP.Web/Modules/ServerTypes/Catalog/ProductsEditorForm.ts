import { DateEditor, initFormType, IntegerEditor, PrefixedContext, StringEditor } from "@serenity-is/corelib";

export interface ProductsEditorForm {
    Code: StringEditor;
    Name: StringEditor;
    Name2: StringEditor;
    Description: StringEditor;
    Barcode: StringEditor;
    Currency: StringEditor;
    VatRate: StringEditor;
    InsertDate: DateEditor;
    InsertUserId: IntegerEditor;
    UpdateDate: DateEditor;
    UpdateUserId: IntegerEditor;
    IsActive: IntegerEditor;
}

export class ProductsEditorForm extends PrefixedContext {
    static readonly formKey = 'Catalog.ProductsEditor';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!ProductsEditorForm.init) {
            ProductsEditorForm.init = true;

            var w0 = StringEditor;
            var w1 = DateEditor;
            var w2 = IntegerEditor;

            initFormType(ProductsEditorForm, [
                'Code', w0,
                'Name', w0,
                'Name2', w0,
                'Description', w0,
                'Barcode', w0,
                'Currency', w0,
                'VatRate', w0,
                'InsertDate', w1,
                'InsertUserId', w2,
                'UpdateDate', w1,
                'UpdateUserId', w2,
                'IsActive', w2
            ]);
        }
    }
}