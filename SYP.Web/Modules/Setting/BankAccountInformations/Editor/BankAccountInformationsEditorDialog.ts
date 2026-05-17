import { EditorDialogBase } from '@/Common/Editors/EditorDialogBase';
import { Decorators } from '@serenity-is/corelib';
import { BankAccountInformationsEditorForm, BankAccountInformationsRow } from '../../ServerTypes/Setting';

@Decorators.registerClass('SYP.Setting.BankAccountInformationsEditorDialog')
export class BankAccountInformationsEditorDialog extends EditorDialogBase<BankAccountInformationsRow> {
    protected getFormKey() { return BankAccountInformationsEditorForm.formKey; }
    protected getRowDefinition() { return BankAccountInformationsRow; }

    protected form = new BankAccountInformationsEditorForm(this.idPrefix);
}