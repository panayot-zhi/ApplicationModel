// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Concepts;

namespace Domain.Carts;

/// <summary>
/// Represents an implementation of <see cref="ICart"/>.
/// </summary>
public class Cart : Grain, ICart
{
    /// <inheritdoc/>
    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Activated");
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task AddItem(SKU sku, Price price, Quantity quantity)
    {
        return Task.CompletedTask;
    }
}