import { Decorators, EntityGrid} from '@serenity-is/corelib';
import { EmailLogsColumns, EmailLogsRow, EmailLogsService } from '../../ServerTypes/Email';
import { EmailLogsDialog } from './EmailLogsDialog';

@Decorators.registerClass('SYP.Email.EmailLogsGrid')
export class EmailLogsGrid extends EntityGrid<EmailLogsRow, any> {
    protected getColumnsKey() { return EmailLogsColumns.columnsKey; }
    protected getDialogType() { return EmailLogsDialog; }
    protected getRowDefinition() { return EmailLogsRow; }
    protected getService() { return EmailLogsService.baseUrl; }

    constructor(props: any) {
        super(props);
    }

    protected getAddButtonCaption() {
        return null; // Log kayıtları manuel eklenemez
    }
}
