// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Cratis.Applications.Identity.for_IdentityProviderEndpointExtensions.when_adding_identity_provider;

public class and_there_is_one_provider : Specification
{
    class MyIdentityProvider : IProvideIdentityDetails
    {
        public Task<IdentityDetails> Provide(IdentityProviderContext context) => throw new NotImplementedException();
    }

    IServiceCollection services;
    ITypes types;
    ServiceDescriptor _serviceDescriptor;

    void Establish()
    {
        services = Substitute.For<IServiceCollection>();
        types = Substitute.For<ITypes>();
        types.FindMultiple<IProvideIdentityDetails>().Returns([typeof(MyIdentityProvider)]);
        services
            .When(_ => _.Add(Arg.Any<ServiceDescriptor>()))
            .Do(_ => _serviceDescriptor = _.Arg<ServiceDescriptor>());
    }

    void Because() => services.AddIdentityProvider(types);

    [Fact] void should_add_one_service_registration() => services.Received(1).Add(Arg.Any<ServiceDescriptor>());
    [Fact] void should_register_as_identity_details_provider() => _serviceDescriptor.ServiceType.ShouldEqual(typeof(IProvideIdentityDetails));
    [Fact] void should_register_expected_provider() => _serviceDescriptor.ImplementationType.ShouldEqual(typeof(MyIdentityProvider));
    [Fact] void should_register_as_singleton() => _serviceDescriptor.Lifetime.ShouldEqual(ServiceLifetime.Singleton);
}