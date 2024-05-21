// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Concepts;
using Concepts.Orders;

namespace Read.Orders;

/// <summary>
/// Represents a line in an order.
/// </summary>
/// <param name="SKU"><see cref="SKU"/> representing the product.</param>
/// <param name="Price">The <see cref="Price"/> of each item.</param>
/// <param name="Quantity"><see cref="Quantity"/> for the line.</param>
public record OrderLine(SKU SKU, Price Price, Quantity Quantity);
