// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Concepts;
using Concepts.Carts;

namespace Domain.Carts;

/// <summary>
/// Defines a system that can handle a cart.
/// </summary>
public interface ICart : IGrainWithGuidKey
{
    /// <summary>
    /// Add an item to the cart.
    /// </summary>
    /// <param name="sku">The Sku to add.</param>
    /// <param name="price">The price per item.</param>
    /// <param name="quantity">Number of items to add.</param>
    /// <returns>Awaitable task.</returns>
    Task AddItem(SKU sku, Price price, Quantity quantity);
}
