import { Decorators, ToolButton, notifySuccess, notifyError, serviceRequest } from '@serenity-is/corelib';
import { EntityDialog } from '@serenity-is/corelib';
import { SmtpSettingsForm, SmtpSettingsRow, SmtpSettingsService } from '../../ServerTypes/Email';

@Decorators.registerClass('SYP.Email.SmtpSettingsDialog')
export class SmtpSettingsDialog extends EntityDialog<SmtpSettingsRow, any> {
    protected getFormKey() { return SmtpSettingsForm.formKey; }
    protected getRowDefinition() { return SmtpSettingsRow; }
    protected getService() { return SmtpSettingsService.baseUrl; }

    protected form = new SmtpSettingsForm(this.idPrefix);

    constructor(opt?: any) {
        super(opt);
    }

    protected getToolbarButtons(): ToolButton[] {
        let buttons = super.getToolbarButtons();

        buttons.push({
            title: 'Test Email Gönder',
            cssClass: 'test-email-button',
            icon: 'fa-paper-plane text-blue',
            onClick: () => {
                this.testEmailClick();
            }
        });

        return buttons;
    }

    protected updateInterface(): void {
        super.updateInterface();

        // Test butonu sadece kayıtlı SMTP ayarları için görünsün
        this.toolbar.findButton('test-email-button').toggle(this.isEditMode());
    }

    private testEmailClick(): void {
        if (!this.entity?.Id) {
            notifyError('Önce SMTP ayarlarını kaydedin!');
            return;
        }

        const email = window.prompt('Test email adresi girin:');

        if (!email) {
            return;
        }

        if (!this.isValidEmail(email)) {
            notifyError('Geçerli bir email adresi girin!');
            return;
        }

        serviceRequest(SmtpSettingsService.baseUrl + '/TestEmail', {
            SmtpSettingsId: this.entity.Id,
            ToEmail: email
        }, (response: any) => {
            if (response.Success) {
                notifySuccess(response.Message);
            } else {
                notifyError(response.Message);
            }
        }, {
            onError: (err: any) => {
                notifyError('Bağlantı hatası: ' + (err.Error?.Message || 'Bilinmeyen hata'));
            }
        });
    }

    private isValidEmail(email: string): boolean {
        return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);
    }
}
