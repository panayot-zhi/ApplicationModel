// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Cratis.MongoDB;

/// <summary>
/// Defines the builder for MongoDB.
/// </summary>
public interface IMongoDBBuilder
{
    /// <summary>
    /// Gets the class maps to register.
    /// </summary>
    IList<Type> ClassMaps { get; }

    /// <summary>
    /// Gets the convention pack filters to register.
    /// </summary>
    IList<Type> ConventionPackFilters { get; }
}