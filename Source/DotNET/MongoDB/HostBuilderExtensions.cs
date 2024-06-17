// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Cratis.Applications.MongoDB;
using Cratis.Models;
using Cratis.MongoDB;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Microsoft.Extensions.Hosting;

/// <summary>
/// Provides extension methods for <see cref="IHostBuilder"/>.
/// </summary>
public static class HostBuilderExtensions
{
    static readonly IDictionary<string, IMongoClient> _clients = new Dictionary<string, IMongoClient>();
    static IMongoDBClientFactory? _clientFactory;
    static IMongoServerResolver? _serverResolver;

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

        mongoDBBuilder.Validate();

        builder.ConfigureServices((context, services) =>
        {
            services.AddSingleton(mongoDBBuilder.ModelNameResolver ?? new ModelNameResolver(new DefaultModelNameConvention()));
            services.AddHostedService<MongoDBInitializer>();
            services.AddSingleton(mongoDBBuilder.ServerResolver!);
            services.AddSingleton(mongoDBBuilder.DatabaseNameResolver!);
            services.AddSingleton<IMongoDBClientFactory, MongoDBClientFactory>();
            services.AddTransient(sp =>
            {
                _clientFactory ??= sp.GetRequiredService<IMongoDBClientFactory>();
                _serverResolver ??= sp.GetRequiredService<IMongoServerResolver>();
                var server = _serverResolver.Resolve();
                if (_clients.TryGetValue(server.ToString(), out var client))
                {
                    return client;
                }

                client = _clientFactory.Create();
                return _clients[server.ToString()] = client;
            });

            services.AddTransient(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(mongoDBBuilder.DatabaseNameResolver!.Resolve());
            });

            services.AddTransient(typeof(IMongoCollection<>), typeof(MongoCollectionAdapter<>));
        });
        return builder;
    }
}
