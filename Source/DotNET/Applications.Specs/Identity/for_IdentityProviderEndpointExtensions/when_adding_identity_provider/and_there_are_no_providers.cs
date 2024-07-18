// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Cratis.Applications.Identity.for_IdentityProviderEndpointExtensions.when_adding_identity_provider;

public class and_there_are_no_providers : Specification
{
    IServiceCollection services;
    ITypes types;
    ServiceDescriptor service_descriptor;

    void Establish()
    {
        services = Substitute.For<IServiceCollection>();
        types = Substitute.For<ITypes>();
        services
            .When(_ => _.Add(Arg.Any<ServiceDescriptor>()))
            .Do(_ => service_descriptor = _.Arg<ServiceDescriptor>());
    }

    void Because() => services.AddIdentityProvider(types);

    [Fact] void should_add_one_service_registration() => services.Received(1).Add(Arg.Any<ServiceDescriptor>());
    [Fact] void should_add_default_identity_details_provider() => service_descriptor.ImplementationType.ShouldEqual(typeof(DefaultIdentityDetailsProvider));
}