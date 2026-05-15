import { Decorators, ToolButton, confirmDialog, notifySuccess, serviceRequest } from '@serenity-is/corelib';
import { EntityDialog } from '@serenity-is/corelib';
import { EmailQueueForm, EmailQueueRow, EmailQueueService } from '../../ServerTypes/Email';

@Decorators.registerClass('SYP.Email.EmailQueueDialog')
export class EmailQueueDialog extends EntityDialog<EmailQueueRow, any> {
    protected getFormKey() { return EmailQueueForm.formKey; }
    protected getRowDefinition() { return EmailQueueRow; }
    protected getService() { return EmailQueueService.baseUrl; }

    protected form = new EmailQueueForm(this.idPrefix);

    constructor(opt?: any) {
        super(opt);
    }

    protected getToolbarButtons(): ToolButton[] {
        let buttons = super.getToolbarButtons();

        buttons.push({
            title: 'İptal Et',
            cssClass: 'cancel-email-button',
            icon: 'fa-times text-danger',
            onClick: () => {
                if (!this.entity?.Id) return;

                confirmDialog('Bu emaili iptal etmek istediğinize emin misiniz?', () => {
                    serviceRequest(EmailQueueService.baseUrl + '/CancelEmail', {
                        EmailId: this.entity.Id
                    }, () => {
                        notifySuccess('Email iptal edildi.');
                        this.reloadById();
                    });
                });
            }
        });

        buttons.push({
            title: 'Tekrar Gönder',
            cssClass: 'resend-email-button',
            icon: 'fa-refresh text-blue',
            onClick: () => {
                if (!this.entity?.Id) return;

                confirmDialog('Bu emaili tekrar kuyruğa eklemek istediğinize emin misiniz?', () => {
                    serviceRequest(EmailQueueService.baseUrl + '/ResendEmail', {
                        EmailId: this.entity.Id
                    }, () => {
                        notifySuccess('Email tekrar kuyruğa eklendi.');
                        this.reloadById();
                    });
                });
            }
        });

        return buttons;
    }
}
