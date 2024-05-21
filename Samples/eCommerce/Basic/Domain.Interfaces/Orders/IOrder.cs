// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Concepts;
using Concepts.Orders;
using Cratis.Applications.Orleans.StateMachines;
using Read.Carts;
using Read.Orders;

namespace Domain.Orders;

/// <summary>
/// Defines a specific order.
/// </summary>
public interface IOrder : IStateMachine<Order>, IGrainWithGuidKey
{
    /// <summary>
    /// Initiate an order from a given cart.
    /// </summary>
    /// <param name="cart">The <see cref="Cart"/> to initiate from.</param>
    /// <returns>Awaitable task.</returns>
    Task Initiate(Cart cart);

    /// <summary>
    /// Set the delivery address for the order.
    /// </summary>
    /// <param name="recipient">The <see cref="Recipient"/> for the delivery.</param>
    /// <returns>Awaitable task.</returns>
    Task SetDeliveryRecipient(Recipient recipient);

    /// <summary>
    /// Set the invoice address for the order.
    /// </summary>
    /// <param name="recipient">The <see cref="Recipient"/> for the invoice.</param>
    /// <returns>Awaitable task.</returns>
    Task SetInvoiceRecipient(Recipient recipient);

    /// <summary>
    /// Start authorizing payment.
    /// </summary>
    /// <returns>Awaitable task.</returns>
    Task AuthorizePayment();

    /// <summary>
    /// Payment has been authorized.
    /// </summary>
    /// <returns>Awaitable task.</returns>
    Task PaymentAuthorized();

    /// <summary>
    /// Order has been sent.
    /// </summary>
    /// <returns>Awaitable task.</returns>
    Task Sent();

    /// <summary>
    /// Cancel the order.
    /// </summary>
    /// <returns>Awaitable task.</returns>
    Task Cancel();
}