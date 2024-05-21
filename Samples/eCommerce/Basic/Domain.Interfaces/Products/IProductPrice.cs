// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Concepts;

namespace Domain.Products;

/// <summary>
/// Defines the pricing in the eCommerce.
/// </summary>
public interface IProductPrice : IGrainWithStringKey
{
    /// <summary>
    /// Set the price for a product.
    /// </summary>
    /// <param name="price">Price to set.</param>
    /// <returns>Awaitable task.</returns>
    Task SetPrice(Price price);

    /// <summary>
    /// Get the price for a product.
    /// </summary>
    /// <param name="sku">Product <see cref="SKU"/> to get the price for.</param>
    /// <returns>Price for the product.</returns>
    Task<Price> GetPrice();
}