// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Concepts.Carts;
using Cratis.Applications.ModelBinding;
using Domain.Carts;

namespace API.Carts;

/// <summary>
/// Represents the cart API.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="Cart"/>.
/// </remarks>
/// <param name="grainFactory"><see cref="IGrainFactory"/> for working with grains.</param>
[Route("/api/carts")]
public class Cart(IGrainFactory grainFactory) : ControllerBase
{
    /// <summary>
    /// Add an item to the cart.
    /// </summary>
    /// <param name="addItemToCart">Payload holding the command.</param>
    /// <returns>Awaitable task.</returns>
    [HttpPost("add-item")]
    public Task AddItem(
        [FromBody] AddItemToCart addItemToCart) =>
        grainFactory.GetGrain<ICart>(Guid.Parse("198a3ee5-c10c-49b0-a2f4-41646c6a5fe2")).AddItem(addItemToCart.Sku, 0, addItemToCart.Quantity);
}
