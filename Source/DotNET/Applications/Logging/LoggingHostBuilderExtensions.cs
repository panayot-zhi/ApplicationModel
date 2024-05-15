// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;

namespace Microsoft.Extensions.Hosting;

/// <summary>
/// Extension methods for configuring logging for a host.
/// </summary>
public static class LoggingHostBuilderExtensions
{
    /// <summary>
    /// Use default logging.
    /// </summary>
    /// <param name="builder"><see cref="IHostBuilder"/> to use with.</param>
    /// <param name="configuration">Optional <see cref="IConfiguration"/> used for the host. Logging configuration will be loaded from this.</param>
    /// <returns><see cref="ILoggerFactory"/> for continuation.</returns>
    public static ILoggerFactory UseDefaultLogging(this IHostBuilder builder, IConfiguration? configuration = default)
    {
        var loggerConfiguration = new LoggerConfiguration()
            .Enrich.WithExceptionDetails();

        if (configuration is not null)
        {
            loggerConfiguration = loggerConfiguration.ReadFrom.Configuration(configuration);
        }

        Log.Logger = loggerConfiguration.CreateLogger();

        builder.UseSerilog();

        Serilog.Debugging.SelfLog.Enable(Console.WriteLine);

        return new Serilog.Extensions.Logging.SerilogLoggerFactory();
    }

    /// <summary>
    /// Use default logging.
    /// </summary>
    /// <param name="builder"><see cref="WebApplicationBuilder"/> to use with.</param>
    /// <returns><see cref="ILoggerFactory"/> for continuation.</returns>
    public static ILoggerFactory UseDefaultLogging(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration().Enrich.WithExceptionDetails()
            .Enrich.WithExceptionDetails()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(Log.Logger);

        Serilog.Debugging.SelfLog.Enable(Console.WriteLine);

        return new Serilog.Extensions.Logging.SerilogLoggerFactory();
    }
}
