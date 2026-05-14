import { GridEditorBase } from '@/_Ext/Editors/GridEditorBase';
import { Decorators } from '@serenity-is/corelib';
import { CustomersEditorColumns, CustomersRow } from '../../ServerTypes/Customer';
import { CustomersEditorDialog } from './CustomersEditorDialog';

@Decorators.registerEditor('SYP.Customer.CustomersGridEditor')
export class CustomersGridEditor extends GridEditorBase<CustomersRow> {
    protected getColumnsKey() { return CustomersEditorColumns.columnsKey; }
    protected getDialogType() { return CustomersEditorDialog; }
    protected getRowDefinition() { return CustomersRow; }

    constructor(props: any) {
        super(props);
    }
}