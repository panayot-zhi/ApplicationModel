// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Cratis.Applications.Identity;
using Cratis.Types;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension methods for setting up the identity provider.
/// </summary>
public static class IdentityProviderServiceCollectionExtensions
{
    /// <summary>
    /// Add a identity provider to the service collection.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> to add to.</param>
    /// <param name="types"><see cref="ITypes"/> for discovering identity provider.</param>
    /// <returns><see cref="IServiceCollection"/> for continuation.</returns>
    /// <exception cref="MultipleIdentityDetailsProvidersFound">Thrown if multiple identity details providers are found.</exception>
    public static IServiceCollection AddIdentityProvider(this IServiceCollection services, ITypes types)
    {
        var providerTypes = types.FindMultiple<IProvideIdentityDetails>().ToArray();
        if (providerTypes.Length > 1)
        {
            throw new MultipleIdentityDetailsProvidersFound(providerTypes);
        }

        if (providerTypes.Length == 1)
        {
            services.AddSingleton(typeof(IProvideIdentityDetails), providerTypes[0]);
        }
        else
        {
            services.AddSingleton<IProvideIdentityDetails, DefaultIdentityDetailsProvider>();
        }

        return services;
    }
}