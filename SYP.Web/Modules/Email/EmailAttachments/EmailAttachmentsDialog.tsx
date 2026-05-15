import { Decorators } from '@serenity-is/corelib';
import { EntityDialog } from '@serenity-is/corelib';
import { EmailAttachmentsForm, EmailAttachmentsRow, EmailAttachmentsService } from '../../ServerTypes/Email';

@Decorators.registerClass('SYP.Email.EmailAttachmentsDialog')
export class EmailAttachmentsDialog extends EntityDialog<EmailAttachmentsRow, any> {
    protected getFormKey() { return EmailAttachmentsForm.formKey; }
    protected getRowDefinition() { return EmailAttachmentsRow; }
    protected getService() { return EmailAttachmentsService.baseUrl; }

    protected form = new EmailAttachmentsForm(this.idPrefix);

    constructor(opt?: any) {
        super(opt);
    }

    protected getSaveState(): string {
        return ''; // Salt okunur - kaydetme yok
    }

    protected getDeleteOptions() {
        return null; // Silme yok
    }
}
