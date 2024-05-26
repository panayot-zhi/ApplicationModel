// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Cratis.Applications;
using Cratis.Conversion;
using Cratis.DependencyInjection;
using Cratis.Json;
using Cratis.Serialization;
using Cratis.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting;

/// <summary>
/// Provides extension methods for <see cref="IHostBuilder"/>.
/// </summary>
public static class HostBuilderExtensions
{
    /// <summary>
    /// Use Cratis ApplicationModel with the <see cref="WebApplicationBuilder"/>.
    /// </summary>
    /// <param name="builder"><see cref="IHostBuilder"/> to extend.</param>
    /// <returns><see cref="IHostBuilder"/> for building continuation.</returns>
    public static WebApplicationBuilder UseApplicationModel(
        this WebApplicationBuilder builder)
    {
        builder.Host.UseApplicationModel();
        return builder;
    }

    /// <summary>
    /// Use Cratis ApplicationModel with the <see cref="IHostBuilder"/>.
    /// </summary>
    /// <param name="builder"><see cref="IHostBuilder"/> to extend.</param>
    /// <returns><see cref="IHostBuilder"/> for building continuation.</returns>
    public static IHostBuilder UseApplicationModel(this IHostBuilder builder)
    {
        Internals.Types = Types.Instance;
        Internals.Types.RegisterTypeConvertersForConcepts();
        TypeConverters.Register();
        var derivedTypes = DerivedTypes.Instance;

        Globals.Configure(derivedTypes);

        builder.UseDefaultServiceProvider(_ => _.ValidateOnBuild = false);

        builder
            .ConfigureServices(_ =>
            {
                _
                .AddSingleton(Internals.Types)
                .AddSingleton<IDerivedTypes>(derivedTypes)
                .AddIdentityProvider(Internals.Types)
                .AddControllersFromProjectReferencedAssembles(Internals.Types, derivedTypes)
                .AddBindingsByConvention()
                .AddSelfBindings();
            });

        return builder;
    }
}
