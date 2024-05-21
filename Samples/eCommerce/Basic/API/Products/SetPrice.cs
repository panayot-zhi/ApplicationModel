// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Concepts;

namespace API.Products;

/// <summary>
/// The command for setting the price of a product.
/// </summary>
/// <param name="SKU">The SKU of the product.</param>
/// <param name="Price">The price to set.</param>
public record SetPrice(SKU SKU, Price Price);
