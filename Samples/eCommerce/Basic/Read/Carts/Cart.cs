// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Concepts;
using Concepts.Carts;

namespace Read.Carts;

/// <summary>
/// Represents the cart for a customer.
/// </summary>
public class Cart
{
    /// <summary>
    /// Gets or sets the <see cref="CartId"/> for the cart.
    /// </summary>
    public CartId Id { get; set; } = CartId.NotSet;

    /// <summary>
    /// Gets or sets the items in the cart.
    /// </summary>
    public IList<CartItem> Items { get; set; } = [];
}
