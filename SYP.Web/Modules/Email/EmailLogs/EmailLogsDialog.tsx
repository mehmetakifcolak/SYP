import { Decorators } from '@serenity-is/corelib';
import { EntityDialog } from '@serenity-is/corelib';
import { EmailLogsForm, EmailLogsRow, EmailLogsService } from '../../ServerTypes/Email';

@Decorators.registerClass('SYP.Email.EmailLogsDialog')
export class EmailLogsDialog extends EntityDialog<EmailLogsRow, any> {
    protected getFormKey() { return EmailLogsForm.formKey; }
    protected getRowDefinition() { return EmailLogsRow; }
    protected getService() { return EmailLogsService.baseUrl; }

    protected form = new EmailLogsForm(this.idPrefix);

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
