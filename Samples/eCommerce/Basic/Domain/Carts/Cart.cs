// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Concepts;
using Read.Carts;

namespace Domain.Carts;

/// <summary>
/// Represents an implementation of <see cref="ICart"/>.
/// </summary>
public class Cart : Grain<Read.Carts.Cart>, ICart
{
    /// <inheritdoc/>
    public async Task AddItem(SKU sku, Price price, Quantity quantity)
    {
        var existingItem = State.Items.FirstOrDefault(_ => _.SKU == sku && _.Price == price);
        if (existingItem is not null)
        {
            var index = State.Items.IndexOf(existingItem);
            State.Items.Remove(existingItem);
            State.Items.Insert(index, existingItem with { Quantity = existingItem.Quantity + quantity });
        }
        else
        {
            var item = new CartItem(sku, price, quantity);
            State.Items.Add(item);
        }

        await WriteStateAsync();
    }
}
