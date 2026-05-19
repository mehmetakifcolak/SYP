using Hangfire.Dashboard;
using Microsoft.Extensions.DependencyInjection;
using Serenity.Abstractions;

namespace SYP.Common;

public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        var httpContext = context.GetHttpContext();

        if (httpContext.User?.Identity?.IsAuthenticated != true)
            return false;

        var permissions = httpContext.RequestServices.GetService<IPermissionService>();
        return permissions?.HasPermission(Administration.PermissionKeys.Security) == true;
    }
}
