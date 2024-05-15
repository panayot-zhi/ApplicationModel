// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Cratis.Applications;
using Cratis.Conversion;
using Cratis.DependencyInjection;
using Cratis.Json;
using Cratis.Serialization;
using Cratis.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
    /// <param name="mvcOptionsDelegate">Optional delegate if one wants to configure MVC specifics, since this configured MVC automatically.</param>
    /// <returns><see cref="IHostBuilder"/> for building continuation.</returns>
    public static WebApplicationBuilder UseApplicationModel(
        this WebApplicationBuilder builder,
        Action<MvcOptions>? mvcOptionsDelegate = default)
    {
        builder.Host.UseApplicationModel(mvcOptionsDelegate, builder.Configuration);
        return builder;
    }

    /// <summary>
    /// Use Cratis ApplicationModel with the <see cref="IHostBuilder"/>.
    /// </summary>
    /// <param name="builder"><see cref="IHostBuilder"/> to extend.</param>
    /// <param name="mvcOptionsDelegate">Optional delegate if one wants to configure MVC specifics, since this configured MVC automatically.</param>
    /// <param name="configuration"><see cref="IConfiguration"/> used for the host.</param>
    /// <returns><see cref="IHostBuilder"/> for building continuation.</returns>
    public static IHostBuilder UseApplicationModel(
        this IHostBuilder builder,
        Action<MvcOptions>? mvcOptionsDelegate = default,
        IConfiguration? configuration = default)
    {
#pragma warning disable CA2000 // Dispose objects before losing scope => Disposed by the host
        var loggerFactory = builder.UseDefaultLogging(configuration);
#pragma warning restore CA2000
        var logger = loggerFactory.CreateLogger("Cratis setup");
        logger.SettingUpDefaults();

        builder.ConfigureAppConfiguration((context, config) => config.AddJsonFile(Path.Combine("./config", "appsettings.json"), optional: true, reloadOnChange: true));

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
                .AddSelfBindings()
                .AddSwaggerGen(options =>
                {
                    var files = Directory.GetFiles(AppContext.BaseDirectory).Where(file => Path.GetExtension(file) == ".xml");
                    var documentationFiles = files.Where(file =>
                        {
                            var fileName = Path.GetFileNameWithoutExtension(file);
                            var dllFileName = Path.Combine(AppContext.BaseDirectory, $"{fileName}.dll");
                            var xmlFileName = Path.Combine(AppContext.BaseDirectory, $"{fileName}.xml");
                            return File.Exists(dllFileName) && File.Exists(xmlFileName);
                        });

                    foreach (var file in documentationFiles)
                    {
                        options.IncludeXmlComments(file);
                    }
                })
                .AddEndpointsApiExplorer();

                if (mvcOptionsDelegate is not null)
                {
                    _.AddMvc(mvcOptionsDelegate);
                }
                else
                {
                    _.AddMvc();
                }
            });

        return builder;
    }
}
