// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Cratis.MongoDB;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting;

/// <summary>
/// Provides extension methods for <see cref="IHostBuilder"/>.
/// </summary>
public static class HostBuilderExtensions
{
    /// <summary>
    /// Use MongoDB in the solution. Configures default settings for the MongoDB Driver.
    /// </summary>
    /// <param name="builder"><see cref="IHostBuilder"/> to use MongoDB with.</param>
    /// <param name="mongoDBBuilderCallback">Optional builder callback for configuring MongoDB.</param>
    /// <returns><see cref="IHostBuilder"/> for building continuation.</returns>
    /// <remarks>
    /// It will automatically hook up any implementations of <see cref="IBsonClassMapFor{T}"/>
    /// and <see cref="ICanFilterMongoDBConventionPacksForType"/>.
    /// </remarks>
    public static IHostBuilder UseMongoDB(this IHostBuilder builder, Action<IMongoDBBuilder>? mongoDBBuilderCallback = default)
    {
        var mongoDBBuilder = new MongoDBBuilder();
        mongoDBBuilderCallback?.Invoke(mongoDBBuilder);

        MongoDBDefaults.Initialize(mongoDBBuilder);
        builder.ConfigureServices((context, services) => services.AddHostedService<MongoDBInitializer>());
        return builder;
    }
}
