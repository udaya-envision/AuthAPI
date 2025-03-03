#pragma warning disable CS0618 // Type or member is obsolete
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;
using AuthAPI.CustomAttribute;

namespace AuthAPI;

public class NoAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    ISystemClock clock)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder, clock)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var identity = new ClaimsIdentity();
        var principal = new ClaimsPrincipal(identity);
        var permissions = new List<Claim>
        {
            new("Permission", Permissions.Read.ToString()),
            new("Permission", Permissions.Create.ToString())
        };
        identity.AddClaims(permissions);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}