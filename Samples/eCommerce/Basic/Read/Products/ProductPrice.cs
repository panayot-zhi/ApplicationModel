// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Concepts;

namespace Read.Products;

/// <summary>
/// Represents the price of a product.
/// </summary>
public class ProductPrice
{
    /// <summary>
    /// Gets or sets the SKU of the product.
    /// </summary>
    public SKU Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    public Price Price { get; set; } = Price.Zero;
}
