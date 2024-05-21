// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using Cratis.Applications.Orleans.StateMachines;

namespace Domain.Orders.States;

/// <summary>
/// Represents the state of an order being initiated. This is typically after the order has been created with the items from a cart.
/// </summary>
public class Initiated : State<Read.Orders.Order>
{
    /// <inheritdoc/>
    protected override IImmutableList<Type> AllowedTransitions => [typeof(DeliveryRecipientSet)];
}
