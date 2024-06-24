// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Cratis.Models;

namespace Cratis.Applications.MongoDB;

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

    /// <summary>
    /// Configures the MongoDB builder with a <see cref="IMongoServerResolver"/>.
    /// </summary>
    /// <typeparam name="TResolver">The <see cref="IMongoServerResolver"/> type.</typeparam>
    /// <param name="builder">The MongoDB builder.</param>
    /// <returns>The updated MongoDB builder.</returns>
    public static IMongoDBBuilder WithServerResolver<TResolver>(this IMongoDBBuilder builder)
        where TResolver : IMongoServerResolver
    {
        builder.ServerResolverType = typeof(TResolver);
        return builder;
    }

    /// <summary>
    /// Configures the MongoDB builder with a <see cref="IMongoDatabaseNameResolver"/>.
    /// </summary>
    /// <typeparam name="TResolver">The <see cref="IMongoDatabaseNameResolver"/> type.</typeparam>
    /// <param name="builder">The MongoDB builder.</param>
    /// <returns>The updated MongoDB builder.</returns>
    public static IMongoDBBuilder WithDatabaseResolver<TResolver>(this IMongoDBBuilder builder)
        where TResolver : IMongoDatabaseNameResolver
    {
        builder.DatabaseNameResolverType = typeof(TResolver);
        return builder;
    }

    /// <summary>
    /// Configures the MongoDB builder with a <see cref="IModelNameConvention"/>.
    /// </summary>
    /// <typeparam name="TConvention">The <see cref="IModelNameConvention"/> type.</typeparam>
    /// <param name="builder">The MongoDB builder.</param>
    /// <returns>The updated MongoDB builder.</returns>
    public static IMongoDBBuilder WithModelNameConvention<TConvention>(this IMongoDBBuilder builder)
        where TConvention : IModelNameConvention
    {
        builder.ModelNameConventionInstance = null;
        builder.ModelNameConventionType = typeof(TConvention);
        return builder;
    }

    /// <summary>
    /// Configures the MongoDB builder with a <see cref="IModelNameConvention"/>.
    /// </summary>
    /// <param name="builder">The MongoDB builder.</param>
    /// <param name="convention">The <see cref="IModelNameConvention"/>.</param>
    /// <returns>The updated MongoDB builder.</returns>
    public static IMongoDBBuilder WithModelNameConvention(this IMongoDBBuilder builder, IModelNameConvention convention)
    {
        builder.ModelNameConventionInstance = convention;
        builder.ModelNameConventionType = null;
        return builder;
    }
}
