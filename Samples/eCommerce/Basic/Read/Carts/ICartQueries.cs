// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reactive.Subjects;
using Concepts.Carts;

namespace Read.Carts;

/// <summary>
/// Defines the queries for carts.
/// </summary>
public interface ICartQueries
{
    /// <summary>
    /// Get a specific cart.
    /// </summary>
    /// <param name="cartId">The <see cref="CartId"/> for the cart.</param>
    /// <returns>The <see cref="Cart"/>.</returns>
    Task<Cart> Get(CartId cartId);

    /// <summary>
    /// Observe a specific cart.
    /// </summary>
    /// <param name="cartId">The <see cref="CartId"/> for the cart.</param>
    /// <returns>The subject for <see cref="Cart"/>.</returns>
    ISubject<Cart> Observe(CartId cartId);
}
