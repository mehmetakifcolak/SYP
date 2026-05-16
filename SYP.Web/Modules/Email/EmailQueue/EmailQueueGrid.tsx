import { Decorators, ToolButton, confirmDialog, notifySuccess, serviceRequest } from '@serenity-is/corelib';
import { EmailQueueColumns, EmailQueueRow, EmailQueueService } from '../../ServerTypes/Email';
import { EmailQueueDialog } from './EmailQueueDialog';
import { GridBase } from '../../_Ext/Bases/GridBase';

@Decorators.registerClass('SYP.Email.EmailQueueGrid')
export class EmailQueueGrid extends GridBase<EmailQueueRow, any> {
    protected getColumnsKey() { return EmailQueueColumns.columnsKey; }
    protected getDialogType() { return EmailQueueDialog; }
    protected getRowDefinition() { return EmailQueueRow; }
    protected getService() { return EmailQueueService.baseUrl; }

    constructor(props: any) {
        super(props);
    }

    protected getButtons(): ToolButton[] {
        let buttons = super.getButtons();

        buttons.push({
            title: 'Seçilileri İptal Et',
            cssClass: 'cancel-button',
            icon: 'fa-times text-danger',
            onClick: () => {
                let selectedKeys = this.rowSelection.getSelectedKeys();
                if (selectedKeys.length === 0) {
                    notifySuccess('Lütfen en az bir kayıt seçin.');
                    return;
                }

                confirmDialog('Seçili emailleri iptal etmek istediğinize emin misiniz?', () => {
                    for (let key of selectedKeys) {
                        serviceRequest(EmailQueueService.baseUrl + '/CancelEmail', { EmailId: parseInt(key) }, () => {});
                    }
                    this.refresh();
                    notifySuccess('Seçili emailler iptal edildi.');
                });
            }
        });

        return buttons;
    }
}
