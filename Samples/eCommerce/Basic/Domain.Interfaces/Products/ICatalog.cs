// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Concepts;

namespace Domain.Products;

/// <summary>
/// Defines the catalog in the eCommerce.
/// </summary>
public interface ICatalog
{
    /// <summary>
    /// Add a product to the catalog.
    /// </summary>
    /// <param name="sku">Product <see cref="SKU"/> to add.</param>
    /// <param name="name">Name of the product.</param>
    /// <returns>Awaitable task.</returns>
    Task AddProduct(SKU sku, ProductName name);
}
