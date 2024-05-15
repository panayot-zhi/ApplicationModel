// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Concepts;
using Concepts.Carts;

namespace API.Carts;

public record AddItemToCart(SKU Sku, Quantity Quantity);
