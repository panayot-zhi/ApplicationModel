// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reactive.Subjects;
using Concepts.Carts;

namespace Read.Carts;

/// <summary>
/// Represents an implementation of <see cref="ICartQueries"/>.
/// </summary>
/// <param name="collection">The <see cref="IMongoCollection{TDocument}"/> for <see cref="Cart"/>.</param>
public class CartQueries(IMongoCollection<Cart> collection) : ICartQueries
{
    /// <inheritdoc/>
    public async Task<Cart> Get(CartId cartId) => await collection.FindByIdAsync(cartId) ?? new() { Id = cartId };

    /// <inheritdoc/>
    public Task<ISubject<Cart>> Observe(CartId cartId) => collection.ObserveById(cartId);
}