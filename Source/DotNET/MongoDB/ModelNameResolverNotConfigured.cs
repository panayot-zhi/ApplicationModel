// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Cratis.Models;

namespace Cratis.Applications.MongoDB;

/// <summary>
/// The exception that is thrown when the <see cref="IMongoServerResolver"/> is missing.
/// </summary>
/// <param name="message">The additional message of the error.</param>
public class ModelNameResolverNotConfigured(string message)
    : Exception($"A model name convention for MongoDB has not been configured. {message}")
{
    /// <summary>
    /// Throw if not configured.
    /// </summary>
    /// <param name="convention">The <see cref="IModelNameConvention"/>.</param>
    /// <param name="conventionType">The type of the model name convention.</param>
    /// <exception cref="Cratis.Applications.MongoDB.ModelNameResolverNotConfigured">Thrown if the resolver is not configured.</exception>
    public static void ThrowIfNotConfigured(IModelNameConvention? convention, Type? conventionType)
    {
        if (convention is not null)
        {
            if (conventionType is not null)
            {
                throw new ModelNameResolverNotConfigured($"Two model name conventions are configured. Use {nameof(MongoDBBuilderExtensions.WithModelNameConvention)} to configure a model name convention");
            }

            return;
        }

        if (conventionType is null)
        {
            throw new ModelNameResolverNotConfigured($"Please configure it using the {nameof(MongoDBBuilderExtensions.WithModelNameConvention)} method");
        }

        if (!conventionType.IsAssignableTo(typeof(IModelNameResolver)))
        {
            throw new ModelNameResolverNotConfigured($"The given type {conventionType} is not assignable to {typeof(IModelNameConvention)}");
        }
    }
}
