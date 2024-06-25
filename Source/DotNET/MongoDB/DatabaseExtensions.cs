// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Cratis.Applications.MongoDB;
using Cratis.Models;

namespace MongoDB.Driver;

/// <summary>
/// Convenience extension methods for <see cref="IMongoDatabase"/>.
/// </summary>
public static class DatabaseExtensions
{
    static IModelNameResolver? _modelNameResolver;

    /// <summary>
    /// The <see cref="IModelNameResolver"/> to use.
    /// </summary>
    /// <exception cref="ModelNameResolverNotConfigured">Thrown when the resolver is not configured.</exception>
    static IModelNameResolver ModelNameResolver =>
        _modelNameResolver ?? throw new ModelNameResolverNotConfigured($"Cannot use {nameof(IMongoDatabase)}.{nameof(GetCollection)}() before it has been configured. Please configure it using {nameof(MongoDBBuilderExtensions.WithModelNameConvention)}");

    /// <summary>
    /// Get a collection - with name of collection as convention (camelCase of typename).
    /// </summary>
    /// <param name="database"><see cref="IMongoDatabase"/> to extend.</param>
    /// <param name="settings">Optional <see cref="MongoCollectionSettings"/>.</param>
    /// <typeparam name="T">Type of collection to get.</typeparam>
    /// <returns>The collection for your type.</returns>
    public static IMongoCollection<T> GetCollection<T>(this IMongoDatabase database, MongoCollectionSettings? settings = default)
    {
        return database.GetCollection<T>(ModelNameResolver!.GetNameFor(typeof(T)), settings);
    }

    /// <summary>
    /// Sets the <see cref="ModelNameResolver"/>.
    /// </summary>
    /// <param name="resolver">The <see cref="IModelNameResolver"/>.</param>
    internal static void SetModelNameResolver(IModelNameResolver resolver) => _modelNameResolver = resolver;
}
