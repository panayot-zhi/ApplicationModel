// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Cratis.MongoDB;

/// <summary>
/// Provides extension methods for the <see cref="IMongoDBBuilder"/>.
/// </summary>
public static class MongoDBBuilderExtensions
{
    /// <summary>
    /// Adds a class map to the MongoDB builder.
    /// </summary>
    /// <typeparam name="T">The type of the class map to add.</typeparam>
    /// <param name="builder">The MongoDB builder.</param>
    /// <returns>The updated MongoDB builder.</returns>
    public static IMongoDBBuilder AddClassMap<T>(this IMongoDBBuilder builder)
    {
        builder.ClassMaps.Add(typeof(T));
        return builder;
    }

    /// <summary>
    /// Adds a convention pack filter to the MongoDB builder.
    /// </summary>
    /// <typeparam name="T">The type of the convention pack filter to add.</typeparam>
    /// <param name="builder">The MongoDB builder.</param>
    /// <returns>The updated MongoDB builder.</returns>
    public static IMongoDBBuilder AddConventionPackFilter<T>(this IMongoDBBuilder builder)
    {
        builder.ConventionPackFilters.Add(typeof(T));
        return builder;
    }
}