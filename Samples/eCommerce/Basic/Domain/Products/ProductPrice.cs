// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Concepts;

namespace Domain.Products;

/// <summary>
/// Represents an implementation of <see cref="IProductPrice"/>.
/// </summary>
public class ProductPrice : Grain<Read.Products.ProductPrice>, IProductPrice
{
    /// <inheritdoc/>
    public Task<Price> GetPrice() => Task.FromResult(State.Price);

    /// <inheritdoc/>
    public Task SetPrice(Price price)
    {
        State.Price = price;
        return WriteStateAsync();
    }
}
