import { EditorDialogBase } from '@/Common/Editors/EditorDialogBase';
import { Decorators } from '@serenity-is/corelib';
import { CustomersEditorForm, CustomersRow } from '../../ServerTypes/Customer';

@Decorators.registerClass('SYP.Customer.CustomersEditorDialog')
export class CustomersEditorDialog extends EditorDialogBase<CustomersRow> {
    protected getFormKey() { return CustomersEditorForm.formKey; }
    protected getRowDefinition() { return CustomersRow; }

    protected form = new CustomersEditorForm(this.idPrefix);
}