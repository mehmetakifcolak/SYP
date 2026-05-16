import { Decorators } from '@serenity-is/corelib';
import { EmailAttachmentsColumns, EmailAttachmentsRow, EmailAttachmentsService } from '../../ServerTypes/Email';
import { EmailAttachmentsDialog } from './EmailAttachmentsDialog';
import { GridBase } from '../../_Ext/Bases/GridBase';

@Decorators.registerClass('SYP.Email.EmailAttachmentsGrid')
export class EmailAttachmentsGrid extends GridBase<EmailAttachmentsRow, any> {
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
