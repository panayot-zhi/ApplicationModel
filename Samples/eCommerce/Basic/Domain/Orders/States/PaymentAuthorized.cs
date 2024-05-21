// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Cratis.Applications.Orleans.StateMachines;

namespace Domain.Orders.States;

/// <summary>
/// Represents the state of an order where payment has been authorized.
/// </summary>
public class PaymentAuthorized : State<Read.Orders.Order>;