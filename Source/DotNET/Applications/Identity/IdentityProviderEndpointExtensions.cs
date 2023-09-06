// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using Aksio.Applications.Identity;
using Aksio.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Extension methods for setting up identity providers.
/// </summary>
public static class IdentityProviderEndpointExtensions
{
    /// <summary>
    /// Map identity provider endpoints.
    /// </summary>
    /// <param name="endpoints">Endpoints to extend.</param>
    /// <param name="app"><see cref="IApplicationBuilder"/> adding to.</param>
    /// <returns>Continuation.</returns>
    /// <exception cref="MultipleIdentityDetailsProvidersFound">Thrown if multiple identity details providers are found.</exception>
    public static IEndpointRouteBuilder MapIdentityProvider(this IEndpointRouteBuilder endpoints, IApplicationBuilder app)
    {
        var serializerOptions = app.ApplicationServices.GetService<JsonSerializerOptions>()!;
        var types = app.ApplicationServices.GetService<ITypes>()!;
        var providerTypes = types.FindMultiple<IProvideIdentityDetails>().ToArray();
        if (providerTypes.Length > 1)
        {
            throw new MultipleIdentityDetailsProvidersFound(providerTypes);
        }

        if (providerTypes.Length == 1)
        {
            endpoints.MapGet(".aksio/me", async (HttpRequest request, HttpResponse response) =>
            {
                if (request.Headers.ContainsKey(MicrosoftIdentityPlatformHeaders.IdentityIdHeader) &&
                    request.Headers.ContainsKey(MicrosoftIdentityPlatformHeaders.IdentityNameHeader) &&
                    request.Headers.ContainsKey(MicrosoftIdentityPlatformHeaders.PrincipalHeader))
                {
                    IdentityId identityId = request.Headers[MicrosoftIdentityPlatformHeaders.IdentityIdHeader].ToString();
                    IdentityName identityName = request.Headers[MicrosoftIdentityPlatformHeaders.IdentityNameHeader].ToString();
                    var token = Convert.FromBase64String(request.Headers[MicrosoftIdentityPlatformHeaders.PrincipalHeader]);
                    var tokenAsJson = JsonNode.Parse(token) as JsonObject;

                    if (tokenAsJson is not null && tokenAsJson.TryGetPropertyValue("claims", out var claimsArray) && claimsArray is JsonArray claimsAsArray)
                    {
                        var claims = request.GetClaims().ToDictionary(claim => claim.Type, claim => claim.Value);

                        var provider = (app.ApplicationServices.GetService(providerTypes[0]) as IProvideIdentityDetails)!;
                        var context = new IdentityProviderContext(identityId, identityName, tokenAsJson, claims);
                        var result = await provider.Provide(context);

                        if (result.IsUserAuthorized)
                        {
                            response.StatusCode = 200;
                        }
                        else
                        {
                            response.StatusCode = 403;
                        }

                        response.ContentType = "application/json; charset=utf-8";
                        var options = new JsonSerializerOptions(serializerOptions)
                        {
                            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                        };
                        await response.WriteAsJsonAsync(result.Details, options);
                    }
                }
            });
        }

        return endpoints;
    }
}
