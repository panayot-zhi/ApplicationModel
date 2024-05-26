// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using MongoDB.Driver;

namespace Cratis.MongoDB;

/// <summary>
/// Represents an implementation of <see cref="IMongoServerResolver"/> that resolves a static <see cref="MongoUrl"/>.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="StaticMongoServerResolver"/> class.
/// </remarks>
/// <param name="connectionString">Connection string to use.</param>
public class StaticMongoServerResolver(string connectionString) : IMongoServerResolver
{
    private readonly MongoUrl _url = new(connectionString);

    /// <inheritdoc/>
    public MongoUrl Resolve() => _url;
}