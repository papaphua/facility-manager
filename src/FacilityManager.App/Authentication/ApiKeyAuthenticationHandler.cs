using System.Security.Claims;
using System.Text.Encodings.Web;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace FacilityManager.App.Authentication;

public sealed class ApiKeyAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    private const string AuthHeader = "ApiKey";
    private readonly string _validApiKey = Env.GetString("API_KEY");

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(AuthHeader, out var value))
            return AuthenticateResult.Fail("API Key is missing");

        if (_validApiKey != value.ToString()) return AuthenticateResult.Fail("Invalid API Key");

        var claims = new[] { new Claim(ClaimTypes.Name, "User") };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}