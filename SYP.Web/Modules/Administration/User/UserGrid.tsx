import { Authorization, Lookup, serviceRequest, tryFirst } from "@serenity-is/corelib";
import { PermissionKeys, RoleRow, UserColumns, UserRow, UserService } from "../../ServerTypes/Administration";
import { nsAdministration } from "../../ServerTypes/Namespaces";
import { UserDialog } from "./UserDialog";
import { GridBase } from "../../_Ext/Bases/GridBase";

export class UserGrid extends GridBase<UserRow, any> {
    static override[Symbol.typeInfo] = this.registerClass(nsAdministration);

    protected override getColumnsKey() { return UserColumns.columnsKey; }
    protected override getDialogType() { return UserDialog; }
    protected override getIdProperty() { return UserRow.idProperty; }
    protected override getIsActiveProperty() { return UserRow.isActiveProperty; }
    protected override getLocalTextPrefix() { return UserRow.localTextPrefix; }
    protected override getService() { return UserService.baseUrl; }

    constructor(props: any) {
        super(props);
    }

    protected override getDefaultSortBy() {
        return [UserRow.Fields.Username];
    }

    protected override createIncludeDeletedButton() {
    }

    protected override createColumns() {
        var columns = super.createColumns();

        var roles = tryFirst(columns, x => x.field == UserRow.Fields.Roles);
        if (roles) {
            var rolesLookup: Lookup<RoleRow>;
            RoleRow.getLookupAsync().then(lookup => {
                rolesLookup = lookup;
                this.sleekGrid.invalidate();
            });

            roles.format = ctx => {
                if (!rolesLookup)
                    return <i class="fa fa-spinner"></i>;

                var roleList = (ctx.value || []).map(x => (rolesLookup.itemById[x] || {}).RoleName || "");
                roleList.sort();
                return roleList.map(x => ctx.escape(x)).join(", ");
            };
        }

        if (!Authorization.hasPermission(PermissionKeys.Security))
            return columns;

        columns.push({
            field: "ImpersonateButton",
            name: "",
            width: 38,
            minWidth: 38,
            maxWidth: 38,
            format: ctx => {
                const userId = ctx.item?.UserId;
                if (!userId) return null;
                return (
                    <a
                        href="#"
                        class="btn btn-sm text-secondary"
                        title="Bu kullanıcı olarak giriş yap"
                        rel="noopener noreferrer"
                        onMouseEnter={(e) => {
                            const a = e.currentTarget as HTMLAnchorElement;
                            const now = Date.now();
                            const lastFetch = Number(a.dataset.lastFetch || 0);
                            if (now - lastFetch < 60_000 && a.href.includes("ImpersonateUser")) return;
                            a.dataset.lastFetch = String(now);
                            serviceRequest(
                                "Administration/User/GetImpersonateToken",
                                { UserId: userId },
                                (resp: any) => {
                                    if (resp?.Token) {
                                        a.href = `/Account/ImpersonateUser?token=${resp.Token}`;
                                        a.target = "_blank";
                                    }
                                },
                                { blockUI: false } as any
                            );
                        }}
                        onClick={(e) => {
                            if (!(e.currentTarget as HTMLAnchorElement).href.includes("ImpersonateUser"))
                                e.preventDefault();
                        }}
                    >
                        <i class="fa fa-sign-in" />
                    </a>
                );
            }
        });

        return columns;
    }
}
