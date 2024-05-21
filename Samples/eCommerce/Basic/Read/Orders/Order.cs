// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Concepts;
using Concepts.Orders;
using Cratis.Applications.Orleans.StateMachines;

namespace Read.Orders;

/// <summary>
/// Represents the state of an order.
/// </summary>
public class Order : StateMachineState
{
    /// <summary>
    /// Gets or sets the <see cref="OrderId"/> for the order.
    /// </summary>
    public OrderId Id { get; set; } = OrderId.NotSet;

    /// <summary>
    /// Gets or sets the <see cref="Recipient"/> for delivery of the order.
    /// </summary>
    public Recipient? DeliveryRecipient { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Recipient"/> for invoice of the order.
    /// </summary>
    public Recipient? InvoiceRecipient { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="OrderStatus"/> for the order.
    /// </summary>
    public IList<OrderLine> Lines { get; set; } = [];
}
