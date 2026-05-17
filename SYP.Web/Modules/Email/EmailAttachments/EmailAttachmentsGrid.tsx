import { Decorators, EntityGrid} from '@serenity-is/corelib';
import { EmailAttachmentsColumns, EmailAttachmentsRow, EmailAttachmentsService } from '../../ServerTypes/Email';
import { EmailAttachmentsDialog } from './EmailAttachmentsDialog';

@Decorators.registerClass('SYP.Email.EmailAttachmentsGrid')
export class EmailAttachmentsGrid extends EntityGrid<EmailAttachmentsRow, any> {
    protected getColumnsKey() { return EmailAttachmentsColumns.columnsKey; }
    protected getDialogType() { return EmailAttachmentsDialog; }
    protected getRowDefinition() { return EmailAttachmentsRow; }
    protected getService() { return EmailAttachmentsService.baseUrl; }

    constructor(props: any) {
        super(props);
    }

    protected getAddButtonCaption() {
        return null; // Ekler manuel eklenemez
    }
}
