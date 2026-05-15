import { Decorators, Formatter, ISlickFormatter } from "@serenity-is/corelib"
import { FormatterContext } from "@serenity-is/sleekgrid"

@Decorators.registerFormatter('_Ext.ActivePassiveFormatter', [ISlickFormatter])
export class ActivePassiveFormatter implements Formatter {
    static format(val) {
        if (val == null) return '';

        let valAsBool = Boolean(val);

        return valAsBool
            ? `<span class="label label-success" style="font-size: unset; padding-bottom: 0.1em;">Aktif</span>`
            : `<span class="label label-danger" style="font-size: unset; padding-bottom: 0.1em;">Pasif</span>`;
    }

    format(ctx: FormatterContext) {
        return ActivePassiveFormatter.format(ctx.value);
    }
}
