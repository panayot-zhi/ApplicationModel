// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Cratis.MongoDB;

/// <summary>
/// Represents an implementation of <see cref="IMongoDatabaseNameResolver"/> that resolves to a static database name.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="StaticMongoDatabaseNameResolver"/>.
/// </remarks>
/// <param name="databaseName">Name of the database.</param>
public class StaticMongoDatabaseNameResolver(string databaseName) : IMongoDatabaseNameResolver
{
    private readonly string _databaseName = databaseName;

    /// <inheritdoc/>
    public string Resolve() => _databaseName;
}