// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Cratis.Applications.Orleans.StateMachines.when_checking_if_is_in_state;

public class and_it_is : given.a_state_machine_with_well_known_states
{
    bool result;

    protected override Type initial_state => typeof(StateThatSupportsTransitioningFrom);

    void Because() => result = state_machine.IsInState;

    [Fact] void should_be_in_state() => result.ShouldBeTrue();
}
