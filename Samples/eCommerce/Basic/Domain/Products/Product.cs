// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Concepts;

namespace Domain.Products;

/// <summary>
/// Represents an implementation of <see cref="IProduct"/>.
/// </summary>
public class Product : Grain<Read.Products.Product>, IProduct
{
    /// <inheritdoc/>
    public Task Register(ProductName productName)
    {
        State.Name = productName;
        State.IsRegistered = true;
        return WriteStateAsync();
    }

    /// <inheritdoc/>
    public Task<bool> Exists() => Task.FromResult(State.IsRegistered);
}
