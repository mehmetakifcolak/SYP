import { GridEditorBase } from '@/_Ext/Editors/GridEditorBase';
import { Decorators } from '@serenity-is/corelib';
import { VendorTypeEditorColumns, VendorTypeRow } from '../../ServerTypes/Setting';
import { VendorTypeEditorDialog } from './VendorTypeEditorDialog';

@Decorators.registerEditor('SYP.Setting.VendorTypeGridEditor')
export class VendorTypeGridEditor extends GridEditorBase<VendorTypeRow> {
    protected getColumnsKey() { return VendorTypeEditorColumns.columnsKey; }
    protected getDialogType() { return VendorTypeEditorDialog; }
    protected getRowDefinition() { return VendorTypeRow; }

    constructor(props: any) {
        super(props);
    }
}