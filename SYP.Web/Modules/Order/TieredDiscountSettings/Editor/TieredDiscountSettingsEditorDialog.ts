import { EditorDialogBase } from '@/Common/Editors/EditorDialogBase';
import { Decorators } from '@serenity-is/corelib';
import { TieredDiscountSettingsEditorForm, TieredDiscountSettingsRow } from '../../ServerTypes/Order';

@Decorators.registerClass('SYP.Order.TieredDiscountSettingsEditorDialog')
export class TieredDiscountSettingsEditorDialog extends EditorDialogBase<TieredDiscountSettingsRow> {
    protected getFormKey() { return TieredDiscountSettingsEditorForm.formKey; }
    protected getRowDefinition() { return TieredDiscountSettingsRow; }

    protected form = new TieredDiscountSettingsEditorForm(this.idPrefix);
}