using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SYP.Membership.Pages;

public partial class AccountPage
{
    [HttpGet]
    public async Task<ActionResult> ImpersonateUser(
        string token,
        [FromServices] Administration.ImpersonateTokenService tokenService,
        [FromServices] IUserClaimCreator userClaimCreator,
        [FromServices] ISqlConnections sqlConnections)
    {
        var userId = tokenService.ConsumeToken(token);
        if (userId == null)
            return Redirect("~/Account/Login");

        using var connection = sqlConnections.NewFor<Administration.UserRow>();
        var user = connection.TryFirst<Administration.UserRow>(
            new Criteria(Administration.UserRow.Fields.UserId) == userId.Value);

        if (user?.Username == null)
            return Redirect("~/Account/Login");

        var principal = userClaimCreator.CreatePrincipal(user.Username, authType: "Impersonation");
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        return Redirect("~/");
    }
}
