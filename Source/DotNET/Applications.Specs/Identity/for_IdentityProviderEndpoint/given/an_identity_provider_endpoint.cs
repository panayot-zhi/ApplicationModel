// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Cratis.Applications.Identity.for_IdentityProviderEndpoint.given;

public class an_identity_provider_endpoint : Specification
{
    protected JsonSerializerOptions serializer_options;
    protected IProvideIdentityDetails identity_provider;
    protected IdentityProviderEndpoint endpoint;
    protected HttpRequest request;
    protected HttpResponse response;
    protected HeaderDictionary headers;
    protected MemoryStream body_stream;
    protected HttpContext http_context;

    void Establish()
    {
        http_context = Substitute.For<HttpContext>();
        serializer_options = new JsonSerializerOptions();
        identity_provider = Substitute.For<IProvideIdentityDetails>();
        endpoint = new(serializer_options, identity_provider);

        request = Substitute.For<HttpRequest>();
        request.HttpContext.Returns(http_context);
        headers = [];
        request.Headers.Returns(headers);

        response = Substitute.For<HttpResponse>();
        response.HttpContext.Returns(http_context);
        body_stream = Substitute.For<MemoryStream>();
        response.Body.Returns(body_stream);
    }
}
