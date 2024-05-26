// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Concepts;
using Concepts.Carts;

namespace API.Carts;

/// <summary>
/// The command for adding an item to the cart.
/// </summary>
/// <param name="Sku">The <see cref="SKU"/> for the item.</param>
/// <param name="Quantity"><see cref="Quantity"/> to add.</param>
public record AddItemToCart(SKU Sku, Quantity Quantity);
