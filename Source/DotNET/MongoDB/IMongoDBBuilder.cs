// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Cratis.Models;

namespace Cratis.Applications.MongoDB;

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

    /// <summary>
    /// Gets or sets the URL resolver.
    /// </summary>
    Type? ServerResolverType { get; set; }

    /// <summary>
    /// Gets or sets the database name resolver.
    /// </summary>
    Type? DatabaseNameResolverType { get; set; }

    /// <summary>
    /// Gets or sets the model name resolver. Not specifying this will revert to the default.
    /// </summary>
    IModelNameResolver? ModelNameResolver { get; set; }

    /// <summary>
    /// Validate the builder.
    /// </summary>
    void Validate();
}
