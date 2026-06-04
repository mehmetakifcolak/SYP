import { EditorDialogBase } from '@/Common/Editors/EditorDialogBase';
import { Authorization, Decorators } from '@serenity-is/corelib';
import { OrderEditorForm, OrderRow } from '../../ServerTypes/Order';

@Decorators.registerClass('SYP.Order.OrderEditorDialog')
export class OrderEditorDialog extends EditorDialogBase<OrderRow> {
    protected getFormKey() { return OrderEditorForm.formKey; }
    protected getRowDefinition() { return OrderRow; }

    protected form = new OrderEditorForm(this.idPrefix);

    protected afterLoadEntity() {
        super.afterLoadEntity();

        // Yeni kayıtta, kullanıcı Bayii değilse CustomerId alanını göster ve düzenlenebilir yap
        const isNewRecord = this.isNew();
        const isBayii = Authorization.hasPermission('Administration:Bayii');

        if (isNewRecord) {
            if (isBayii) {
                // Bayii kullanıcıları için CustomerId alanını gizle
                this.form.CustomerId.getGridField().toggle(false);
            } else {
                // Bayii olmayan kullanıcılar için CustomerId alanını göster ve zorunlu yap
                this.form.CustomerId.getGridField().toggle(true);
            }
        }
    }
}