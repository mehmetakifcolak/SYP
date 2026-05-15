import { Decorators, Formatter, ISlickFormatter } from "@serenity-is/corelib";
import { FormatterContext } from "@serenity-is/sleekgrid";

@Decorators.registerFormatter('SYP.Customer.UserStatusFormatter', [ISlickFormatter])
export class UserStatusFormatter implements Formatter {
    format(ctx: FormatterContext) {
        if (ctx.value == null) return '';

        let isActive = !!ctx.value;
        if (isActive)
            return `<i class="fa fa-check-circle" style="color:#28a745;margin:0px 6px;"></i><span class="badge rounded-pill" style="font-size: 0.9em;background-color: #28a745;"> Aktif</span>`;

        return `<i class="fa fa-times-circle" style="color:#dc3545;margin:0px 6px;"></i><span class="badge rounded-pill" style="font-size: 0.9em;background-color: #dc3545;"> Pasif</span>`;
    }
}
