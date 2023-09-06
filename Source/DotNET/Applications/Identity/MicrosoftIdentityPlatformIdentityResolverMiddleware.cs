// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Aksio.Applications.Identity;

/// <summary>
/// Middleware for resolving identity details running in the context of Microsoft Identity Platform.
/// </summary>
public class MicrosoftIdentityPlatformIdentityResolverMiddleware
{
    readonly RequestDelegate _next;

    /// <summary>
    /// Initializes a new instance of the <see cref="MicrosoftIdentityPlatformIdentityResolverMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware.</param>
    public MicrosoftIdentityPlatformIdentityResolverMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Invoke the middleware.
    /// </summary>
    /// <param name="context">The <see cref="HttpContext"/>. </param>
    /// <returns>Awaitable task.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Headers.ContainsKey(MicrosoftIdentityPlatformHeaders.IdentityIdHeader) &&
            context.Request.Headers.ContainsKey(MicrosoftIdentityPlatformHeaders.IdentityNameHeader) &&
            context.Request.Headers.ContainsKey(MicrosoftIdentityPlatformHeaders.PrincipalHeader))
        {
            var claims = context.Request.GetClaims();
            claims = claims
                .RemoveAll(claim => claim.Type == ClaimTypes.NameIdentifier || claim.Type == "sub")
                .Add(new Claim(ClaimTypes.NameIdentifier, context.Request.Headers[MicrosoftIdentityPlatformHeaders.IdentityIdHeader].ToString()))
                .Add(new Claim("sub", context.Request.Headers[MicrosoftIdentityPlatformHeaders.IdentityIdHeader].ToString()));

            context.Request.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
        }

        await _next(context);
    }
}
