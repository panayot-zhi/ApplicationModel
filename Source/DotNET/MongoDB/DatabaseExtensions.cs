// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Cratis.Models;

namespace MongoDB.Driver;

/// <summary>
/// Convenience extension methods for <see cref="IMongoDatabase"/>.
/// </summary>
public static class DatabaseExtensions
{
    // TODO: Some icky stuff setting this thing...

    /// <summary>
    /// The <see cref="IModelNameResolver"/> to use.
    /// </summary>
    internal static IModelNameResolver ModelNameResolver;

    /// <summary>
    /// Get a collection - with name of collection as convention (camelCase of typename).
    /// </summary>
    /// <param name="database"><see cref="IMongoDatabase"/> to extend.</param>
    /// <param name="settings">Optional <see cref="MongoCollectionSettings"/>.</param>
    /// <typeparam name="T">Type of collection to get.</typeparam>
    /// <returns>The collection for your type.</returns>
    public static IMongoCollection<T> GetCollection<T>(this IMongoDatabase database, MongoCollectionSettings? settings = default)
    {
        return database.GetCollection<T>(ModelNameResolver.GetNameFor(typeof(T)), settings);
    }
}
