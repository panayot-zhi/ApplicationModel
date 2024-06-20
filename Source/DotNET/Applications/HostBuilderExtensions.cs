// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Cratis.Applications;
using Cratis.Conversion;
using Cratis.DependencyInjection;
using Cratis.Json;
using Cratis.Serialization;
using Cratis.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting;

/// <summary>
/// Provides extension methods for <see cref="IHostBuilder"/> for configuring the application model services.
/// </summary>
public static class HostBuilderExtensions
{
    /// <summary>
    /// Gets the default section name for the application model configuration.
    /// </summary>
    public const string DefaultApplicationModelSection = "Cratis.ApplicationModel";

    /// <summary>
    /// Use Cratis ApplicationModel with the <see cref="IHostBuilder"/>.
    /// </summary>
    /// <param name="builder"><see cref="IHostBuilder"/> to extend.</param>
    /// <returns><see cref="IHostBuilder"/> for building continuation.</returns>
    public static IHostBuilder UseApplicationModel(this IHostBuilder builder)
    {
        builder.ConfigureServices(_ => _
            .AddOptions<ApplicationModelOptions>()
            .BindConfiguration(DefaultApplicationModelSection)
            .Configure(options =>
            {
            })
            .ValidateDataAnnotations()
            .ValidateOnStart());

        return builder.UseApplicationModelImplementation();
    }

    public static IHostApplicationBuilder UseApplicationModel(this IHostApplicationBuilder builder)
    {
        builder.Services.AddApplicationModel();
    }

    /// <summary>
    /// Use Cratis ApplicationModel with the <see cref="IHostBuilder"/>.
    /// </summary>
    /// <param name="builder"><see cref="IHostBuilder"/> to extend.</param>
    /// <param name="options">An <see cref="ApplicationModelOptions"/> instance.</param>
    /// <returns><see cref="IHostBuilder"/> for building continuation.</returns>
    public static IHostBuilder UseApplicationModel(this IHostBuilder builder, ApplicationModelOptions options)
    {
        builder.ConfigureServices(_ => _.ConfigureOptions(options));
        return builder.UseApplicationModelImplementation();
    }
    
    /// <summary>
    /// Use Cratis ApplicationModel with the <see cref="IHostBuilder"/>.
    /// </summary>
    /// <param name="builder"><see cref="IHostBuilder"/> to extend.</param>
    /// <param name="options">An <see cref="ApplicationModelOptions"/> instance.</param>
    /// <returns><see cref="IHostBuilder"/> for building continuation.</returns>
    public static IHostBuilder UseApplicationModel(this IHostBuilder builder, ApplicationModelOptions options)
    {
        builder.ConfigureServices(_ => _.ConfigureOptions(options));
        return builder.UseApplicationModelImplementation();
    }
    

    /// <summary>
    /// Use Cratis ApplicationModel with the <see cref="IHostBuilder"/>.
    /// </summary>
    /// <param name="builder"><see cref="IHostBuilder"/> to extend.</param>
    /// <param name="configureOptions">Action to configure the <see cref="ApplicationModelOptions"/>.</param>
    /// <returns><see cref="IHostBuilder"/> for building continuation.</returns>
    public static IHostBuilder UseApplicationModel(this IHostBuilder builder, Action<ApplicationModelOptions> configureOptions)
    {
        builder.ConfigureServices(_ => _.Configure(configureOptions));
        return builder.UseApplicationModelImplementation();
    }

    /// <summary>
    /// Use Cratis ApplicationModel with the <see cref="IHostBuilder"/>.
    /// </summary>
    /// <param name="builder"><see cref="IHostBuilder"/> to extend.</param>
    /// <param name="configuration"><see cref="IConfiguration"/> to use for configuration.</param>
    /// <returns><see cref="IHostBuilder"/> for building continuation.</returns>
    public static IHostBuilder UseApplicationModel(this IHostBuilder builder, IConfiguration configuration)
    {
        builder.ConfigureServices(_ => _
            .Configure<ApplicationModelOptions>(configuration));
        return builder.UseApplicationModelImplementation();
    }

    static IHostBuilder UseApplicationModelImplementation(this IHostBuilder builder)
    {
        Internals.Types = Types.Instance;
        Internals.Types.RegisterTypeConvertersForConcepts();
        TypeConverters.Register();
        var derivedTypes = DerivedTypes.Instance;

        Globals.Configure(derivedTypes);

        builder.UseDefaultServiceProvider(_ => _.ValidateOnBuild = false);

        builder
            .ConfigureServices(_ => _
                .AddTypeDiscovery()
                .AddSingleton<IDerivedTypes>(derivedTypes)
                .AddIdentityProvider(Internals.Types)
                .AddControllersFromProjectReferencedAssembles(Internals.Types, derivedTypes)
                .AddBindingsByConvention()
                .AddSelfBindings());

        return builder;
    }
}
