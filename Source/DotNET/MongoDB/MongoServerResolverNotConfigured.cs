// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Cratis.Applications.MongoDB;

/// <summary>
/// The exception that is thrown when the <see cref="IMongoServerResolver"/> is missing.
/// </summary>
public class MongoServerResolverNotConfigured() : Exception("A resolver for MongoDB server has not been configured. Please configure it using the UseMongoDB extension method.")
{
    /// <summary>
    /// Throw if not configured.
    /// </summary>
    /// <param name="resolver">The resolver value to check.</param>
    /// <exception cref="MongoServerResolverNotConfigured">Thrown if the resolver is not configured.</exception>
    public static void ThrowIfNotConfigured(Type? resolver)
    {
        if (resolver is null)
        {
            throw new MongoServerResolverNotConfigured();
        }
    }
}
