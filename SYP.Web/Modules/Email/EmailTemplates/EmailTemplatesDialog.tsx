import { Decorators } from '@serenity-is/corelib';
import { EntityDialog } from '@serenity-is/corelib';
import { EmailTemplatesForm, EmailTemplatesRow, EmailTemplatesService } from '../../ServerTypes/Email';

@Decorators.registerClass('SYP.Email.EmailTemplatesDialog')
export class EmailTemplatesDialog extends EntityDialog<EmailTemplatesRow, any> {
    protected getFormKey() { return EmailTemplatesForm.formKey; }
    protected getRowDefinition() { return EmailTemplatesRow; }
    protected getService() { return EmailTemplatesService.baseUrl; }

    protected form = new EmailTemplatesForm(this.idPrefix);

    constructor(opt?: any) {
        super(opt);
    }
}
