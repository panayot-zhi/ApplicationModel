// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Cratis.Applications.MongoDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
    /// <param name="configureMongoDB">Optional callback for configuring <see cref="MongoDBOptions"/>.</param>
    /// <param name="mongoDBConfigSectionPath">Optional string for the <see cref="MongoDBOptions"/> config section path.</param>
    /// <returns><see cref="IHostBuilder"/> for building continuation.</returns>
    /// <remarks>
    /// It will automatically hook up any implementations of <see cref="IBsonClassMapFor{T}"/>
    /// and <see cref="ICanFilterMongoDBConventionPacksForType"/>.
    /// </remarks>
    public static IHostBuilder UseMongoDB(
        this IHostBuilder builder,
        Action<IMongoDBBuilder>? mongoDBBuilderCallback = default,
        Action<MongoDBOptions>? configureMongoDB = default,
        string? mongoDBConfigSectionPath = null)
    {
        mongoDBConfigSectionPath ??= ConfigurationPath.Combine("Cratis", "MongoDB");

        var mongoDBBuilder = new MongoDBBuilder();
        mongoDBBuilderCallback?.Invoke(mongoDBBuilder);
        mongoDBBuilder.Validate();
        MongoDBDefaults.Initialize(mongoDBBuilder);
        builder.ConfigureServices((_, services) =>
        {
            AddOptions(services, configureMongoDB)
                .BindConfiguration(mongoDBConfigSectionPath);
            services.AddHostedService<MongoDBInitializer>(provider => new MongoDBInitializer(provider, mongoDBBuilder));
            services.AddSingleton(typeof(IMongoServerResolver), mongoDBBuilder.ServerResolverType);
            services.AddSingleton(typeof(IMongoDatabaseNameResolver), mongoDBBuilder.DatabaseNameResolverType);
            services.AddSingleton<IMongoDBClientFactory, MongoDBClientFactory>();
            services.AddTransient(sp =>
            {
                // TODO: This will not work when multi tenant.
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
                var databaseNameResolver = sp.GetRequiredService<IMongoDatabaseNameResolver>();
                return client.GetDatabase(databaseNameResolver.Resolve());
            });

            services.AddTransient(typeof(IMongoCollection<>), typeof(MongoCollectionAdapter<>));
        });
        return builder;
    }

    static OptionsBuilder<MongoDBOptions> AddOptions(IServiceCollection services, Action<MongoDBOptions>? configureOptions = default)
    {
        var builder = services
            .AddOptions<MongoDBOptions>()
            .ValidateOnStart()
            .ValidateDataAnnotations();

        if (configureOptions is not null)
        {
            builder.Configure(configureOptions);
        }

        return builder;
    }
}
