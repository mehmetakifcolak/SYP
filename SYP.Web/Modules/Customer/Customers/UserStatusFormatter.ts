import { Decorators, Formatter, ISlickFormatter } from "@serenity-is/corelib";
import { FormatterContext } from "@serenity-is/sleekgrid";

@Decorators.registerFormatter('SYP.Customer.UserStatusFormatter', [ISlickFormatter])
export class UserStatusFormatter implements Formatter {
    format(ctx: FormatterContext) {
        if (ctx.value == null) return '';

        let isActive = !!ctx.value;
        let color = isActive ? '#28a745' : '#dc3545';

        let wrapper = document.createElement('span');

        let icon = document.createElement('i');
        icon.className = isActive ? 'fa fa-check-circle' : 'fa fa-times-circle';
        icon.style.cssText = `color:${color};margin:0 6px;`;

        let badge = document.createElement('span');
        badge.className = 'badge rounded-pill';
        badge.style.cssText = `font-size:0.9em;background-color:${color};`;
        badge.textContent = isActive ? 'Aktif' : 'Pasif';

        wrapper.appendChild(icon);
        wrapper.appendChild(badge);

        return wrapper;
    }
}
