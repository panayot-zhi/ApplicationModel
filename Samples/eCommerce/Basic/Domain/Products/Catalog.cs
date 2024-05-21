// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Concepts;

namespace Domain.Products;

/// <summary>
/// Represents an implementation of <see cref="ICatalog"/>.
/// </summary>
public class Catalog : Grain, ICatalog
{
    /// <inheritdoc/>
    public async Task AddProduct(SKU sku, ProductName name)
    {
        await GrainFactory.GetGrain<IProduct>(sku).Register(name);
    }
}