// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Cratis.Applications.Identity;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Extension methods for <see cref="IApplicationBuilder"/> for identity.
/// </summary>
public static class MicrosoftIdentityPlatformIdentityApplicationBuilderExtensions
{
    /// <summary>
    /// Use the Microsoft Identity Platform identity resolver.
    /// </summary>
    /// <param name="app"><see cref="IApplicationBuilder"/> to configure.</param>
    /// <returns><see cref="IApplicationBuilder"/> for continuation.</returns>
    public static IApplicationBuilder UseMicrosoftIdentityPlatformIdentityResolver(this IApplicationBuilder app)
    {
        return app.UseMiddleware<MicrosoftIdentityPlatformIdentityResolverMiddleware>();
    }
}