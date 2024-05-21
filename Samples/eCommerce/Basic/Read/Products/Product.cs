// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Concepts;

namespace Read.Products;

/// <summary>
/// Represents a product.
/// </summary>
public class Product
{
    /// <summary>
    /// Gets or sets the SKU of the product.
    /// </summary>
    public SKU Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    public ProductName Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets whether or not the product has been registered.
    /// </summary>
    public bool IsRegistered { get; set; }
}