// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;

namespace Cratis.Applications.MongoDB.for_BsonClassMapExtensions;

public class when_applying_conventions : Specification
{
    class SomeType
    {
        public string SomeProperty { get; init; }
        public string SomeOtherProperty { get; init; }
    }

    BsonClassMap<SomeType> class_map;
    IMemberMapConvention convention;

    void Establish()
    {
        class_map = new BsonClassMap<SomeType>();
        class_map.AutoMap();
        convention = Substitute.For<IMemberMapConvention>();
        ConventionRegistry.Register(Guid.NewGuid().ToString(), new ConventionPack { convention }, type => type == typeof(SomeType));
    }

    void Because() => class_map.ApplyConventions();

    [Fact] void should_have_two_members() => class_map.DeclaredMemberMaps.Count().ShouldEqual(2);
    [Fact] void should_call_convention_for_first_property() => convention.Received(1).Apply(class_map.DeclaredMemberMaps.First());
    [Fact] void should_call_convention_for_second_property() => convention.Received(1).Apply(class_map.DeclaredMemberMaps.ToArray()[1]);
}