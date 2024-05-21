// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Cratis.Applications.Orleans.StateMachines;

namespace Domain.Orders.States;

/// <summary>
/// Represents the state of an order that has been sent.
/// </summary>
public class Sent : State<Read.Orders.Order>;