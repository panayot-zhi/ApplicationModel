// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Concepts;

namespace API.Products;

/// <summary>
/// Represents the command for adding a product to the catalog.
/// </summary>
/// <param name="SKU">The SKU of the product.</param>
/// <param name="Name">The name of the product.</param>
public record AddProduct(SKU SKU, ProductName Name);