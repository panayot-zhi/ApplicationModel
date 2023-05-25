// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO.Compression;
using Aksio.Applications;
using Aksio.Conversion;
using Aksio.DependencyInversion;
using Aksio.Execution;
using Aksio.Json;
using Aksio.Serialization;
using Aksio.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting;

/// <summary>
/// Provides extension methods for <see cref="IHostBuilder"/>.
/// </summary>
public static class HostBuilderExtensions
{
    /// <summary>
    /// Use Aksio defaults with the <see cref="IHostBuilder"/>.
    /// </summary>
    /// <param name="builder"><see cref="IHostBuilder"/> to extend.</param>
    /// <param name="microserviceId">Optional <see cref="MicroserviceId"/> for the running process. Defaults to <see cref="MicroserviceId.Unspecified"/>.</param>
    /// <param name="microserviceName">Optional <see cref="MicroserviceName"/> for the running process. Defaults to <see cref="MicroserviceName.Unspecified"/>.</param>
    /// <param name="mvcOptionsDelegate">Optional delegate if one wants to configure MVC specifics, since this configured MVC automatically.</param>
    /// <returns><see cref="IHostBuilder"/> for building continuation.</returns>
    public static IHostBuilder UseAksio(
        this IHostBuilder builder,
        MicroserviceId? microserviceId = default,
        MicroserviceName? microserviceName = default,
        Action<MvcOptions>? mvcOptionsDelegate = default)
    {
        #pragma warning disable CA2000 // Dispose objects before losing scope => Disposed by the host
        var loggerFactory = builder.UseDefaultLogging();
        var logger = loggerFactory.CreateLogger("Aksio setup");
        logger.SettingUpDefaults();

        builder.ConfigureAppConfiguration((context, config) => config.AddJsonFile(Path.Combine("./config", "appsettings.json"), optional: true, reloadOnChange: true));

        Types.AddAssemblyPrefixesToExclude(
            "AutoMapper",
            "Autofac",
            "Azure",
            "Elasticsearch",
            "FluentValidation",
            "Grpc",
            "Handlebars",
            "NJsonSchema",
            "MongoDB",
            "Orleans",
            "Serilog",
            "Swashbuckle");

        Internals.Types = new Types();
        Internals.Types.RegisterTypeConvertersForConcepts();
        TypeConverters.Register();
        var derivedTypes = new DerivedTypes(Internals.Types);

        microserviceId ??= MicroserviceId.Unspecified;
        microserviceName ??= MicroserviceName.Unspecified;

        Globals.Configure(derivedTypes);

        builder
            .ConfigureServices(_ =>
            {
                _
                .AddSingleton(Internals.Types)
                .AddSingleton<IDerivedTypes>(derivedTypes)
                .AddSingleton<ProviderFor<IServiceProvider>>(() => Internals.ServiceProvider!)
                .AddConfigurationObjects(Internals.Types, searchSubPaths: new[] { "config" }, logger: logger)
                .AddControllersFromProjectReferencedAssembles(Internals.Types, derivedTypes)
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
                .AddEndpointsApiExplorer()
                .AddResponseCompression(options =>
                {
                    options.EnableForHttps = true;
                    options.Providers.Add<BrotliCompressionProvider>();
                    options.Providers.Add<GzipCompressionProvider>();
                })
                .Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.SmallestSize)
                .Configure<BrotliCompressionProviderOptions>(options => options.Level = CompressionLevel.SmallestSize)

                // Todo: Temporarily adding this, due to a bug in .NET 6 (https://www.ingebrigtsen.info/2021/09/29/autofac-asp-net-core-6-hot-reload-debug-crash/) :
                .AddRazorPages();

                if (mvcOptionsDelegate is not null)
                {
                    _.AddMvc(mvcOptionsDelegate);
                }
                else
                {
                    _.AddMvc();
                }
            })
            .UseDefaultDependencyInversion(Internals.Types);

        return builder;
    }
}