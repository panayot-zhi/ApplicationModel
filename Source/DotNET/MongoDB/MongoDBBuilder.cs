// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Cratis.MongoDB;

/// <summary>
/// Represents an implementation of <see cref="IMongoDBBuilder"/>.
/// </summary>
public class MongoDBBuilder : IMongoDBBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MongoDBBuilder"/> class.
    /// </summary>
    public MongoDBBuilder()
    {
        var types = Types.Types.Instance;
        ClassMaps = types.FindMultiple(typeof(IBsonClassMapFor<>)).ToList();
        ConventionPackFilters = types.FindMultiple(typeof(ICanFilterMongoDBConventionPacksForType)).ToList();
    }

    /// <inheritdoc/>
    public IList<Type> ClassMaps { get; }

    /// <inheritdoc/>
    public IList<Type> ConventionPackFilters { get; }
}