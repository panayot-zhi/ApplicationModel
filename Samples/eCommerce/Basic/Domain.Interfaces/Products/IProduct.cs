// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Concepts;

namespace Domain.Products;

/// <summary>
/// Defines the product in the eCommerce.
/// </summary>
public interface IProduct : IGrainWithStringKey
{
    /// <summary>
    /// Register the product.
    /// </summary>
    /// <returns>Awaitable task.</returns>
    Task Register(ProductName productName);

    /// <summary>
    /// Check if the product exists.
    /// </summary>
    /// <returns>True if it does, false if not.</returns>
    Task<bool> Exists();
}