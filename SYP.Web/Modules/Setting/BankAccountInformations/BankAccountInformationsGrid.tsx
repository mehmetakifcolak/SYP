import { GridBase } from '@/_Ext/Bases/GridBase';
import { Decorators } from '@serenity-is/corelib';
import { BankAccountInformationsColumns, BankAccountInformationsRow, BankAccountInformationsService } from '../../ServerTypes/Setting';
import { BankAccountInformationsDialog } from './BankAccountInformationsDialog';

@Decorators.registerClass('SYP.Setting.BankAccountInformationsGrid')
export class BankAccountInformationsGrid extends GridBase<BankAccountInformationsRow, any> {
    protected getColumnsKey() { return BankAccountInformationsColumns.columnsKey; }
    protected getDialogType() { return BankAccountInformationsDialog; }
    protected getRowDefinition() { return BankAccountInformationsRow; }
    protected getService() { return BankAccountInformationsService.baseUrl; }

    constructor(props: any) {
        super(props);
    }
}