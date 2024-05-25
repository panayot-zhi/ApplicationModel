// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Cratis.MongoDB;
using Microsoft.Extensions.Hosting;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Provides extension methods for the <see cref="WebApplicationBuilder"/> for configuring MongoDB.
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Use MongoDB in the solution. Configures default settings for the MongoDB Driver.
    /// </summary>
    /// <param name="builder"><see cref="WebApplicationBuilder"/> to use MongoDB with.</param>
    /// <param name="mongoDBBuilderCallback">Optional builder callback for configuring MongoDB.</param>
    /// <returns><see cref="WebApplicationBuilder"/> for building continuation.</returns>
    /// <remarks>
    /// It will automatically hook up any implementations of <see cref="IBsonClassMapFor{T}"/>
    /// and <see cref="ICanFilterMongoDBConventionPacksForType"/>.
    /// </remarks>
    public static WebApplicationBuilder UseMongoDB(this WebApplicationBuilder builder, Action<IMongoDBBuilder>? mongoDBBuilderCallback = default)
    {
        builder.Host.UseMongoDB(mongoDBBuilderCallback);
        return builder;
    }
}
