// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Concepts;
using Read.Orders;

namespace Read.Carts;

/// <summary>
/// Represents an item in a cart.
/// </summary>
/// <param name="SKU"><see cref="SKU"/> representing the product.</param>
/// <param name="Price">The <see cref="Price"/> of each item.</param>
/// <param name="Quantity"><see cref="Quantity"/> of the item.</param>
public record CartItem(SKU SKU, Price Price, Quantity Quantity);
