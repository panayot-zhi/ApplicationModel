// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Cratis.Models;

namespace Cratis.Applications.MongoDB;

/// <summary>
/// The exception that is thrown when the <see cref="IMongoServerResolver"/> is missing.
/// </summary>
/// <param name="message">The additional message of the error.</param>
public class ModelNameConventionNotConfigured(string message)
    : Exception($"A model name convention for MongoDB has not been configured. {message}")
{
    /// <summary>
    /// Throw if not configured.
    /// </summary>
    /// <param name="resolver">The resolver value to check.</param>
    /// <exception cref="ModelNameConventionNotConfigured">Thrown if the resolver is not configured.</exception>
    public static void ThrowIfNotConfigured(Type? resolver)
    {
        if (resolver is null)
        {
            throw new ModelNameConventionNotConfigured("Please configure it using the UseMongoDB extension method");
        }

        if (!resolver.IsAssignableTo(typeof(IModelNameResolver)))
        {
            throw new ModelNameConventionNotConfigured($"The type is not assignable to {typeof(IModelNameResolver)}");
        }
    }
}