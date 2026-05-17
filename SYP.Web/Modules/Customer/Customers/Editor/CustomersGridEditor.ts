import { GridEditorBase } from '@/Common/Editors/GridEditorBase';
import { Decorators } from '@serenity-is/corelib';
import { CustomersEditorDialog } from './CustomersEditorDialog';
import { CustomersRow, CustomersEditorColumns } from '../../../ServerTypes/Customer';

@Decorators.registerEditor('SYP.Customer.CustomersGridEditor')
export class CustomersGridEditor extends GridEditorBase<CustomersRow> {
    protected getColumnsKey() { return CustomersEditorColumns.columnsKey; }
    protected getDialogType() { return CustomersEditorDialog; }
    protected getRowDefinition() { return CustomersRow; }

    constructor(props: any) {
        super(props);
    }
}