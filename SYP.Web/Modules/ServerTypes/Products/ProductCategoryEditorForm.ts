import { BooleanEditor, DateEditor, initFormType, IntegerEditor, LookupEditor, PrefixedContext, StringEditor } from "@serenity-is/corelib";

export interface ProductCategoryEditorForm {
    ParentId: LookupEditor;
    Name: StringEditor;
    Path: StringEditor;
    FullPath: StringEditor;
    SortOrder: IntegerEditor;
    IsActive: BooleanEditor;
    CreatedAt: DateEditor;
}

export class ProductCategoryEditorForm extends PrefixedContext {
    static readonly formKey = 'Products.ProductCategoryEditor';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!ProductCategoryEditorForm.init) {
            ProductCategoryEditorForm.init = true;

            var w0 = LookupEditor;
            var w1 = StringEditor;
            var w2 = IntegerEditor;
            var w3 = BooleanEditor;
            var w4 = DateEditor;

            initFormType(ProductCategoryEditorForm, [
                'ParentId', w0,
                'Name', w1,
                'Path', w1,
                'FullPath', w1,
                'SortOrder', w2,
                'IsActive', w3,
                'CreatedAt', w4
            ]);
        }
    }
}