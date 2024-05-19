// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Cratis.Applications.Orleans.MongoDB;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Orleans.Configuration;
using Orleans.Providers;
using Orleans.Runtime.Hosting;

namespace Orleans.Hosting;

/// <summary>
/// Extension methods for configuring MongoDB storage.
/// </summary>
public static class MongoDBGrainStorageExtensions
{
    /// <summary>
    /// Adds MongoDB grain storage as default.
    /// </summary>
    /// <param name="builder"><see cref="ISiloBuilder"/> to add to.</param>
    /// <param name="configureOptions">Action for getting <see cref="MongoDBGrainStorageOptions"/>.</param>
    /// <returns><see cref="ISiloBuilder"/> for continuation.</returns>
    public static ISiloBuilder AddMongoDBStorageAsDefault(this ISiloBuilder builder, Action<MongoDBGrainStorageOptions> configureOptions) =>
        AddMongoDBStorageAsDefault(builder, ob => ob.Configure(configureOptions));

    /// <summary>
    /// Adds MongoDB grain storage as default.
    /// </summary>
    /// <param name="builder"><see cref="ISiloBuilder"/> to add to.</param>
    /// <param name="configureOptions">Action for getting <see cref="MongoDBGrainStorageOptions"/>.</param>
    /// <returns><see cref="ISiloBuilder"/> for continuation.</returns>
    public static ISiloBuilder AddMongoDBStorageAsDefault(this ISiloBuilder builder, Action<OptionsBuilder<MongoDBGrainStorageOptions>>? configureOptions) =>
        AddMongoDBStorage(builder, ProviderConstants.DEFAULT_STORAGE_PROVIDER_NAME, configureOptions);

    /// <summary>
    /// Adds MongoDB grain storage.
    /// </summary>
    /// <param name="builder"><see cref="ISiloBuilder"/> to add to.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="configureOptions">Action for getting <see cref="MongoDBGrainStorageOptions"/>.</param>
    /// <returns><see cref="ISiloBuilder"/> for continuation.</returns>
    public static ISiloBuilder AddMongoDBStorage(this ISiloBuilder builder, string providerName, Action<MongoDBGrainStorageOptions> configureOptions) =>
        AddMongoDBStorage(builder, providerName, ob => ob.Configure(configureOptions));

    /// <summary>
    /// Adds MongoDB grain storage.
    /// </summary>
    /// <param name="builder"><see cref="ISiloBuilder"/> to add to.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="configureOptions">Action for building <see cref="MongoDBGrainStorageOptions"/>.</param>
    /// <returns><see cref="ISiloBuilder"/> for continuation.</returns>
    public static ISiloBuilder AddMongoDBStorage(this ISiloBuilder builder, string providerName, Action<OptionsBuilder<MongoDBGrainStorageOptions>>? configureOptions)
    {
        builder.ConfigureServices(services =>
        {
            configureOptions?.Invoke(services.AddOptions<MongoDBGrainStorageOptions>(providerName));
            services.ConfigureNamedOptionForLogging<MongoDBGrainStorageOptions>(providerName);
            services.AddGrainStorage(providerName, MongoDBGrainStorageFactory.Create);
        });

        return builder;
    }
}