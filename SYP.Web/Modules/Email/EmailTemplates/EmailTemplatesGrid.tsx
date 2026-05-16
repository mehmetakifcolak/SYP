import { Decorators } from '@serenity-is/corelib';
import { EmailTemplatesColumns, EmailTemplatesRow, EmailTemplatesService } from '../../ServerTypes/Email';
import { EmailTemplatesDialog } from './EmailTemplatesDialog';
import { GridBase } from '../../_Ext/Bases/GridBase';

@Decorators.registerClass('SYP.Email.EmailTemplatesGrid')
export class EmailTemplatesGrid extends GridBase<EmailTemplatesRow, any> {
    protected getColumnsKey() { return EmailTemplatesColumns.columnsKey; }
    protected getDialogType() { return EmailTemplatesDialog; }
    protected getRowDefinition() { return EmailTemplatesRow; }
    protected getService() { return EmailTemplatesService.baseUrl; }

    constructor(props: any) {
        super(props);
    }
}
