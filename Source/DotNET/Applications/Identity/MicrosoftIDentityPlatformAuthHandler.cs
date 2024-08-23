// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cratis.Applications.Identity;

/// <summary>
/// Represents an <see cref="AuthenticationHandler{TOptions}"/> for handling authentication in the context of Microsoft Identity Platform.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="MicrosoftIDentityPlatformAuthHandler"/> class.
/// </remarks>
/// <param name="options">The <see cref="IOptionsMonitor{TOptions}"/>.</param>
/// <param name="logger">The <see cref="ILoggerFactory"/>.</param>
/// <param name="encoder">The <see cref="UrlEncoder"/>.</param>
/// <param name="clock">The <see cref="ISystemClock"/>.</param>
public class MicrosoftIDentityPlatformAuthHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    ISystemClock clock) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder, clock)
{
    /// <summary>
    /// Gets the scheme name.
    /// </summary>
    public const string SchemeName = "MicrosoftIdentityPlatform";

    /// <inheritdoc/>
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey(MicrosoftIdentityPlatformHeaders.IdentityIdHeader) ||
            !Request.Headers.ContainsKey(MicrosoftIdentityPlatformHeaders.IdentityNameHeader) ||
            !Request.Headers.ContainsKey(MicrosoftIdentityPlatformHeaders.PrincipalHeader))
        {
            return Task.FromResult(AuthenticateResult.Fail("Not authenticated - headers missing"));
        }

        var claims = Request.GetClaims();
        claims = claims
            .RemoveAll(claim => claim.Type == ClaimTypes.NameIdentifier || claim.Type == "sub")
            .Add(new Claim(ClaimTypes.NameIdentifier, Request.Headers[MicrosoftIdentityPlatformHeaders.IdentityIdHeader].ToString()))
            .Add(new Claim("sub", Request.Headers[MicrosoftIdentityPlatformHeaders.IdentityIdHeader].ToString()));

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
