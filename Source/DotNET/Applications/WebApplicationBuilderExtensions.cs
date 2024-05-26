// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Cratis.Applications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Provides extension methods for <see cref="WebApplicationBuilder"/> for configuring the application model services.
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Use Cratis ApplicationModel with the <see cref="WebApplicationBuilder"/>.
    /// </summary>
    /// <param name="builder"><see cref="WebApplicationBuilder"/> to extend.</param>
    /// <returns><see cref="WebApplicationBuilder"/> for building continuation.</returns>
    public static WebApplicationBuilder UseApplicationModel(this WebApplicationBuilder builder)
    {
        builder.Host.UseApplicationModel();
        return builder;
    }

    /// <summary>
    /// Use Cratis ApplicationModel with the <see cref="WebApplicationBuilder"/>.
    /// </summary>
    /// <param name="builder"><see cref="WebApplicationBuilder"/> to extend.</param>
    /// <param name="options">An instance of <see cref="ApplicationModelOptions"/>.</param>
    /// <returns><see cref="WebApplicationBuilder"/> for building continuation.</returns>
    public static WebApplicationBuilder UseApplicationModel(this WebApplicationBuilder builder, ApplicationModelOptions options)
    {
        builder.Host.UseApplicationModel(options);
        return builder;
    }

    /// <summary>
    /// Use Cratis ApplicationModel with the <see cref="WebApplicationBuilder"/>.
    /// </summary>
    /// <param name="builder"><see cref="WebApplicationBuilder"/> to extend.</param>
    /// <param name="configureOptions">Action to configure the <see cref="ApplicationModelOptions"/>.</param>
    /// <returns><see cref="WebApplicationBuilder"/> for building continuation.</returns>
    public static WebApplicationBuilder UseApplicationModel(this WebApplicationBuilder builder, Action<ApplicationModelOptions> configureOptions)
    {
        builder.Host.UseApplicationModel(configureOptions);
        return builder;
    }

    /// <summary>
    /// Use Cratis ApplicationModel with the <see cref="WebApplicationBuilder"/>.
    /// </summary>
    /// <param name="builder"><see cref="WebApplicationBuilder"/> to extend.</param>
    /// <param name="configuration"><see cref="IConfiguration"/> to use for configuration.</param>
    /// <returns><see cref="WebApplicationBuilder"/> for building continuation.</returns>
    public static WebApplicationBuilder UseApplicationModel(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        builder.Host.UseApplicationModel(configuration);
        return builder;
    }
}