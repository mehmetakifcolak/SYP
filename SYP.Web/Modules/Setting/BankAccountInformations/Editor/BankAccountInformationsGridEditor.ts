import { GridEditorBase } from '@/_Ext/Editors/GridEditorBase';
import { Decorators } from '@serenity-is/corelib';
import { BankAccountInformationsEditorColumns, BankAccountInformationsRow } from '../../ServerTypes/Setting';
import { BankAccountInformationsEditorDialog } from './BankAccountInformationsEditorDialog';

@Decorators.registerEditor('SYP.Setting.BankAccountInformationsGridEditor')
export class BankAccountInformationsGridEditor extends GridEditorBase<BankAccountInformationsRow> {
    protected getColumnsKey() { return BankAccountInformationsEditorColumns.columnsKey; }
    protected getDialogType() { return BankAccountInformationsEditorDialog; }
    protected getRowDefinition() { return BankAccountInformationsRow; }

    constructor(props: any) {
        super(props);
    }
}